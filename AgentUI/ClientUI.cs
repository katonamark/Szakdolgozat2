using Microsoft.AspNetCore.SignalR.Client;
using System.Net.Http.Json;
using System.Diagnostics;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace AgentUI
{
    public partial class ClientUI : Form
    {
        private HubConnection? connection;
        private readonly string machineName = Environment.MachineName;
        private readonly HttpClient client = new HttpClient();
        private string logFilePath =>
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs", $"agent_log_{DateTime.Now:yyyyMMdd}.txt");
        [DllImport("user32.dll")]
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, UIntPtr dwExtraInfo);

        private const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const uint MOUSEEVENTF_LEFTUP = 0x0004;


        public ClientUI()
        {
            InitializeComponent();
            lblMachineName.Text = $"Gépnév: {Environment.MachineName}";
            lblUserName.Text = $"Felhasználó: {Environment.UserName}";
            lblOsVersion.Text = $"OS: {Environment.OSVersion}";
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
                .WithUrl("https://localhost:7294/agenthub")
                .WithAutomaticReconnect()
                .Build();

            connection.On<string>("ReceiveMessage", (message) =>
            {
                Invoke(new Action(() =>
                {
                    rtbChatHistory.AppendText(
                        $"[{DateTime.Now:yyyy.MM.dd HH:mm}] Management: {message}{Environment.NewLine}");
                    AddLog("Új üzenet érkezett a szervertõl.");
                }));
            });


            connection.On<string, byte[], string>("ReceiveFile", async (fileName, fileData, targetPath) =>
            {
                try
                {
                    string finalFolder;

                    switch (targetPath)
                    {
                        case "Desktop":
                            finalFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                            break;
                        case "Documents":
                            finalFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                            break;
                        case "Temp":
                            finalFolder = Path.GetTempPath();
                            break;
                        default:
                            finalFolder = targetPath;
                            break;
                    }

                    Directory.CreateDirectory(finalFolder);

                    string fullPath = Path.Combine(finalFolder, fileName);
                    await connection.InvokeAsync(
                        "SendFileResultToManagement",
                        machineName,
                        $"Fájl mentve ide: {fullPath}"
                    );
                    notifyIcon1.BalloonTipTitle = "Fájl érkezett";
                    notifyIcon1.BalloonTipText = fullPath;
                    notifyIcon1.ShowBalloonTip(3000);

                    if (File.Exists(fullPath))
                    {
                        string newFileName =
                            Path.GetFileNameWithoutExtension(fileName) +
                            "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") +
                            Path.GetExtension(fileName);

                        fullPath = Path.Combine(finalFolder, newFileName);
                    }

                    File.WriteAllBytes(fullPath, fileData);

                    Invoke(new Action(() =>
                    {
                        AddLog($"Fájl érkezett: {fullPath}"); ;
                    }));
                }
                catch (Exception ex)
                {
                    Invoke(new Action(() =>
                    {
                        MessageBox.Show("Hiba fájl mentésekor: " + ex.Message);
                    }));
                }

            });
            connection.On<string>("ReceiveCommand", async (command) =>
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
                }
                catch (Exception ex)
                {
                    result = "Hiba: " + ex.Message;
                }

                await connection.InvokeAsync("SendCommandResultToManagement", machineName, result);
            });
            connection.On("TakeScreenshot", async () =>
            {
                try
                {
                    Rectangle bounds = Screen.PrimaryScreen.Bounds;

                    using Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height);
                    using Graphics g = Graphics.FromImage(bitmap);
                    g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);

                    using MemoryStream ms = new MemoryStream();
                    bitmap.Save(ms, ImageFormat.Jpeg);

                    byte[] imageBytes = ms.ToArray();

                    await connection.InvokeAsync("SendScreenshotToManagement", machineName, imageBytes);

                    Invoke(new Action(() =>
                    {
                        AddLog("Képernyõkép elküldve a szervernek.");
                    }));
                }
                catch (Exception ex)
                {
                    Invoke(new Action(() =>
                    {
                        AddLog("Hiba képernyõkép készítésekor: " + ex.Message);
                    }));
                }
            });

            connection.On<int, int>("ReceiveMouseClick", async (x, y) =>
            {
                try
                {
                    Cursor.Position = new Point(x, y);

                    mouse_event(MOUSEEVENTF_LEFTDOWN, (uint)x, (uint)y, 0, UIntPtr.Zero);
                    mouse_event(MOUSEEVENTF_LEFTUP, (uint)x, (uint)y, 0, UIntPtr.Zero);

                    AddLog($"Távoli kattintás végrehajtva: X={x}, Y={y}");

                    // kattintás után új screenshot küldése
                    Rectangle bounds = Screen.PrimaryScreen.Bounds;

                    using Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height);
                    using Graphics g = Graphics.FromImage(bitmap);
                    g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);

                    using MemoryStream ms = new MemoryStream();
                    bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

                    byte[] imageBytes = ms.ToArray();

                    await connection.InvokeAsync("SendScreenshotToManagement", machineName, imageBytes);
                }
                catch (Exception ex)
                {
                    AddLog("Hiba távoli kattintáskor: " + ex.Message);
                }
            });

            try
            {
                await connection.StartAsync();
                await connection.InvokeAsync(
                "RegisterAgent",
                machineName,
                Environment.OSVersion.ToString(),
                Environment.UserName);
                lblStatus.Text = $"Kapcsolódva: {machineName}";
                AddLog("Kapcsolódás a szerverhez sikeres.");
                LoadConversation();
            }


            catch (Exception ex)
            {
                lblStatus.Text = "Kapcsolati hiba";
                AddLog("Kapcsolódás a szerverhez sikeres.");
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

            if (connection == null)
            {
                MessageBox.Show("Nincs kapcsolat a szerverrel.");
                return;
            }

            string messageToSend = txtNewMessage.Text;

            try
            {
                await connection.InvokeAsync("SendMessageToManagement", machineName, messageToSend);

                rtbChatHistory.AppendText(
                    $"[{DateTime.Now:yyyy.MM.dd HH:mm}] {machineName}: {messageToSend}{Environment.NewLine}");

                txtNewMessage.Clear();
                AddLog("Üzenet elküldve a szervernek.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba üzenetküldéskor: " + ex.Message);
            }
        }
        private async void LoadConversation()
        {
            try
            {
                var messages = await client.GetFromJsonAsync<List<ChatRecord>>(
                    $"https://localhost:7294/api/chat/agent/{machineName}");

                rtbChatHistory.Clear();

                if (messages != null)
                {
                    foreach (var msg in messages)
                    {
                        rtbChatHistory.AppendText(
                            $"[{msg.Timestamp:yyyy.MM.dd HH:mm}] {msg.Sender}: {msg.Message}{Environment.NewLine}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba az elõzmények betöltésekor: " + ex.Message);
            }
        }
        private void AddLog(string message)
        {
            string logEntry = $"[{DateTime.Now:yyyy.MM.dd HH:mm:ss}] {message}";

            rtbLog.AppendText(logEntry + Environment.NewLine);

            try
            {
                string? folder = Path.GetDirectoryName(logFilePath);
                if (!string.IsNullOrEmpty(folder))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(logFilePath)!);
                }

                File.AppendAllText(logFilePath, logEntry + Environment.NewLine, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba a log mentésekor: " + ex.Message);
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

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}