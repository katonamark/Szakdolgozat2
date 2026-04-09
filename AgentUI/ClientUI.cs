using Microsoft.AspNetCore.SignalR.Client;
using System.Net.Http.Json;
using System.Diagnostics;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;
using System.Net.Sockets;

namespace AgentUI
{
    public partial class ClientUI : Form
    {
        private HubConnection? connection;
        private readonly string machineName = Environment.MachineName;
        private readonly HttpClient client = new HttpClient();
        private const string SharedSecret = "my-szakdolgozat-super-secret-password-2026-mark";
        private static readonly object LogLock = new object();
        private bool _remoteControlActive;
        private DateTime _lastRemoteControlNotification = DateTime.MinValue;
        private string logFilePath =>
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs", $"agent_log_{DateTime.Now:yyyyMMdd}.txt");

        public ClientUI()
        {
            InitializeComponent();

            lblMachineName.Text = $"Gépnév: {Environment.MachineName}";
            lblUserName.Text = $"Felhasználó: {Environment.UserName}";
            lblOsVersion.Text = $"OS: {RuntimeInformation.OSDescription}";

            txtNewMessage.KeyDown += txtNewMessage_KeyDown;

            LoadLogHistory();
            ConnectToServer();
        }

        public class ChatRecord
        {
            public string Sender { get; set; } = "";
            public string TargetAgent { get; set; } = "";
            public string Message { get; set; } = "";
            public DateTime Timestamp { get; set; }
        }

        private async void ConnectToServer()
        {
            connection = new HubConnectionBuilder()
                .WithUrl(AppConfig.HubUrl)
                .WithAutomaticReconnect()
                .Build();

            connection.Reconnecting += error =>
            {
                BeginInvoke(new Action(() =>
                {
                    lblStatus.Text = "Újrakapcsolódás...";
                    AddLog("Kapcsolat megszakadt, újrakapcsolódás folyamatban.");
                }));

                return Task.CompletedTask;
            };

            connection.Reconnected += connectionId =>
            {
                BeginInvoke(new Action(async () =>
                {
                    try
                    {
                        if (connection != null)
                        {
                            await connection.InvokeAsync(
                                "RegisterAgent",
                                machineName,
                                RuntimeInformation.OSDescription,
                                Environment.UserName,
                                SharedSecret);

                            lblStatus.Text = $"Kapcsolódva: {machineName}";
                            AddLog("Újrakapcsolódás sikeres, agent újraregisztrálva.");
                        }
                    }
                    catch (Exception ex)
                    {
                        lblStatus.Text = "Újrakapcsolódási hiba";
                        AddLog("Hiba újracsatlakozás után: " + ex.Message);
                    }
                }));

                return Task.CompletedTask;
            };

            connection.Closed += error =>
            {
                BeginInvoke(new Action(() =>
                {
                    lblStatus.Text = "Kapcsolat lezárva";
                    AddLog("A kapcsolat lezárult.");
                }));

                return Task.CompletedTask;
            };

            connection.On<string>(" Message", message =>
            {
                BeginInvoke(new Action(() =>
                {
                    AppendChatLine($"[{DateTime.Now:yyyy.MM.dd HH:mm}] Management: {message}");

                    AddLog("Új üzenet érkezett a szervertõl.");
                    ShowNotification("Új üzenet", "Új üzenet érkezett a szervertõl.");
                }));
            });

            connection.On<string, byte[], string>("ReceiveFile", async (fileName, fileData, targetPath) =>
            {
                try
                {
                    string finalFolder = targetPath switch
                    {
                        "Desktop" => Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                        "Documents" => Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                        "Temp" => Path.GetTempPath(),
                        _ => targetPath
                    };

                    Directory.CreateDirectory(finalFolder);

                    string fullPath = Path.Combine(finalFolder, fileName);

                    if (File.Exists(fullPath))
                    {
                        string newFileName =
                            Path.GetFileNameWithoutExtension(fileName) +
                            "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") +
                            Path.GetExtension(fileName);

                        fullPath = Path.Combine(finalFolder, newFileName);
                    }

                    await File.WriteAllBytesAsync(fullPath, fileData);

                    if (connection != null)
                    {
                        await connection.InvokeAsync(
                            "SendFileResultToManagement",
                            machineName,
                            $"Fájl mentve ide: {fullPath}");
                    }

                    BeginInvoke(new Action(() =>
                    {
                        ShowNotification("Fájl érkezett", $"Fájl mentve ide: {fullPath}");

                        AddLog($"Fájl érkezett és elmentve: {fullPath}");
                    }));
                }
                catch (Exception ex)
                {
                    BeginInvoke(new Action(() =>
                    {
                        AddLog("Hiba fájl mentésekor: " + ex.Message);
                        MessageBox.Show("Hiba fájl mentésekor: " + ex.Message);
                    }));
                }
            });

            connection.On<string>("ReceiveCommand", async command =>
            {
                string result;

                try
                {
                    ProcessStartInfo psi = new ProcessStartInfo
                    {
                        FileName = "cmd.exe",
                        Arguments = $"/c {command}",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    };

                    using Process process = new Process();
                    process.StartInfo = psi;
                    process.Start();

                    string output = await process.StandardOutput.ReadToEndAsync();
                    string error = await process.StandardError.ReadToEndAsync();

                    process.WaitForExit();

                    result = string.IsNullOrWhiteSpace(error) ? output : error;

                    AddLog($"Parancs lefuttatva: {command}");
                }
                catch (Exception ex)
                {
                    result = "Hiba: " + ex.Message;
                    AddLog($"Hiba parancsvégrehajtáskor: {ex.Message}");
                }

                if (connection != null)
                {
                    await connection.InvokeAsync("SendCommandResultToManagement", machineName, result);
                }
            });

            connection.On("TakeScreenshot", async () =>
            {
                try
                {
                    byte[] imageBytes = ScreenCaptureService.CapturePrimaryScreenJpeg(60L);

                    if (connection != null)
                    {
                        await connection.InvokeAsync("SendScreenshotToManagement", machineName, imageBytes);
                    }

                    /*BeginInvoke(new Action(() =>
                    {
                        AddLog("Képernyõkép elküldve a szervernek.");
                    }));*/
                }
                catch (Exception ex)
                {
                    BeginInvoke(new Action(() =>
                    {
                        AddLog("Hiba képernyõkép készítésekor: " + ex.Message);
                    }));
                }
            });

            connection.On<string, int, int, int>("ReceiveMouseAction", (action, x, y, delta) =>
            {
                try
                {
                    RemoteInputService.ExecuteMouseAction(action, x, y, delta);
                    MarkRemoteControlActive();

                    /*BeginInvoke(new Action(() =>
                    {
                        AddLog($"Távoli egérmûvelet végrehajtva: action={action}, x={x}, y={y}, delta={delta}");
                    }));*/
                }
                catch (Exception ex)
                {
                    BeginInvoke(new Action(() =>
                    {
                        AddLog("Hiba a távoli egérmûvelet közben: " + ex.Message);
                        MessageBox.Show("Hiba a távoli egérmûvelet közben: " + ex.Message);
                    }));
                }
            });

            connection.On<int, bool, bool, bool, bool>("ReceiveKeyEvent", (keyCode, keyDown, ctrl, alt, shift) =>
            {
                try
                {
                    RemoteInputService.ExecuteKeyEvent(keyCode, keyDown, ctrl, alt, shift);
                    MarkRemoteControlActive();

                    /*BeginInvoke(new Action(() =>
                    {
                        AddLog($"Távoli billentyûmûvelet végrehajtva: keyCode={keyCode}, keyDown={keyDown}, ctrl={ctrl}, alt={alt}, shift={shift}");
                    }));*/
                }
                catch (Exception ex)
                {
                    BeginInvoke(new Action(() =>
                    {
                        AddLog("Hiba a távoli billentyûmûvelet közben: " + ex.Message);
                        MessageBox.Show("Hiba a távoli billentyûmûvelet közben: " + ex.Message);
                    }));
                }
            });

            try
            {
                await connection.StartAsync();

                await connection.InvokeAsync(
                    "RegisterAgent",
                    machineName,
                    RuntimeInformation.OSDescription,
                    Environment.UserName,
                    SharedSecret);

                lblStatus.Text = $"Kapcsolódva: {machineName}";
                AddLog("Kapcsolódás a szerverhez sikeres.");

                LoadConversation();
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Kapcsolati hiba";
                AddLog("Kapcsolódási hiba: " + ex.Message);
                MessageBox.Show("Hiba: " + ex.Message);
            }
        }

        private void LoadLogHistory()
        {
            try
            {
                if (File.Exists(logFilePath))
                {
                    rtbLog.Text = File.ReadAllText(logFilePath, Encoding.UTF8);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba a log betöltésekor: " + ex.Message);
            }
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNewMessage.Text))
            {
                MessageBox.Show("Írj be egy üzenetet.");
                return;
            }

            if (connection == null || connection.State != HubConnectionState.Connected)
            {
                MessageBox.Show("Nincs kapcsolat a szerverrel.");
                return;
            }

            string messageToSend = txtNewMessage.Text.Trim();

            try
            {
                await connection.InvokeAsync("SendMessageToManagement", machineName, messageToSend);

                AppendChatLine($"[{DateTime.Now:yyyy.MM.dd HH:mm}] {machineName}: {messageToSend}");

                txtNewMessage.Clear();
                AddLog("Üzenet elküldve a szervernek.");
            }
            catch (Exception ex)
            {
                AddLog("Hiba üzenetküldéskor: " + ex.Message);
                MessageBox.Show("Hiba üzenetküldéskor: " + ex.Message);
            }
        }

        private async void LoadConversation()
        {
            try
            {
                var messages = await client.GetFromJsonAsync<List<ChatRecord>>(
                   $"{AppConfig.ChatAgentApiBaseUrl}{machineName}");

                rtbChatHistory.Clear();

                if (messages != null)
                {
                    foreach (var msg in messages.OrderBy(m => m.Timestamp))
                    {
                        AppendChatLine($"[{DateTime.Now:yyyy.MM.dd HH:mm}] Management: {msg.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                AddLog("Hiba az elõzmények betöltésekor: " + ex.Message);
                MessageBox.Show("Hiba az elõzmények betöltésekor: " + ex.Message);
            }
        }
        private void AppendChatLine(string text)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => AppendChatLine(text)));
                return;
            }

            rtbChatHistory.AppendText(text + Environment.NewLine);
            rtbChatHistory.SelectionStart = rtbChatHistory.TextLength;
            rtbChatHistory.ScrollToCaret();
        }

        private void AddLog(string message)
        {
            string logEntry = $"[{DateTime.Now:yyyy.MM.dd HH:mm:ss}] {message}";

            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    rtbLog.AppendText(logEntry + Environment.NewLine);
                }));
            }
            else
            {
                rtbLog.AppendText(logEntry + Environment.NewLine);
            }

            try
            {
                string? folder = Path.GetDirectoryName(logFilePath);
                if (!string.IsNullOrWhiteSpace(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                lock (LogLock)
                {
                    File.AppendAllText(logFilePath, logEntry + Environment.NewLine, Encoding.UTF8);
                }
            }
            catch (Exception ex)
            {
                if (InvokeRequired)
                {
                    BeginInvoke(new Action(() =>
                    {
                        MessageBox.Show("Hiba a log mentésekor: " + ex.Message);
                    }));
                }
                else
                {
                    MessageBox.Show("Hiba a log mentésekor: " + ex.Message);
                }
            }
        }

        private void txtNewMessage_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && e.Control)
            {
                return;
            }

            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnSend.PerformClick();
            }
        }

        private async Task SendUpdatedScreenshot()
        {
            try
            {
                byte[] imageBytes = ScreenCaptureService.CapturePrimaryScreenJpeg(60L);

                if (connection != null && connection.State == HubConnectionState.Connected)
                {
                    await connection.InvokeAsync("SendScreenshotToManagement", machineName, imageBytes);
                }
            }
            catch (Exception ex)
            {
                AddLog("Hiba screenshot frissítéskor: " + ex.Message);
            }
        }
        private void MarkRemoteControlActive()
        {
            if (!_remoteControlActive || DateTime.Now - _lastRemoteControlNotification > TimeSpan.FromMinutes(1))
            {
                _remoteControlActive = true;
                _lastRemoteControlNotification = DateTime.Now;

                AddLog("A szerver átvette a vezérlést ezen a gépen.");
                ShowNotification("Távoli vezérlés", "A szerver átvette a vezérlést ezen a gépen.");
            }
        }
        private void ShowNotification(string title, string message, ToolTipIcon icon = ToolTipIcon.Info)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => ShowNotification(title, message, icon)));
                return;
            }

            notifyIcon1.BalloonTipTitle = title;
            notifyIcon1.BalloonTipText = message;
            notifyIcon1.BalloonTipIcon = icon;
            notifyIcon1.Visible = true;
            notifyIcon1.ShowBalloonTip(3000);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}