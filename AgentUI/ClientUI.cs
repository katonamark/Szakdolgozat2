using Microsoft.AspNetCore.SignalR.Client;
using System.Net.Http.Json;
using System.Diagnostics;

namespace AgentUI
{
    public partial class ClientUI : Form
    {
        private HubConnection? connection;
        private readonly string machineName = Environment.MachineName;
        private readonly HttpClient client = new HttpClient();


        public ClientUI()
        {
            InitializeComponent();
            txtNewMessage.KeyDown += txtNewMessage_KeyDown;
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
                }));
            });

            connection.On<string, byte[], string>("ReceiveFile", (fileName, fileData, targetPath) =>
            {
                try
                {
                    Directory.CreateDirectory(targetPath);

                    string fullPath = Path.Combine(targetPath, fileName);
                    File.WriteAllBytes(fullPath, fileData);

                    Invoke(new Action(() =>
                    {
                        MessageBox.Show($"Fájl érkezett ide: {fullPath}");
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

            try
            {
                await connection.StartAsync();
                await connection.InvokeAsync(
                "RegisterAgent",
                machineName,
                Environment.OSVersion.ToString(),
                Environment.UserName);
                lblStatus.Text = $"Kapcsolódva: {machineName}";
                LoadConversation();
            }


            catch (Exception ex)
            {
                lblStatus.Text = "Kapcsolati hiba";
                MessageBox.Show("Hiba: " + ex.Message);
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
        private void txtNewMessage_KeyDown(object sender, KeyEventArgs e)
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