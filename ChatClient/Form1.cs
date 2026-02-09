using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatClient
{
    public partial class Form1 : Form
    {
        private readonly HttpClient client = new HttpClient();
        private readonly string serverUrl = "http://localhost:5260/"; // Saját szerver URL

        public Form1()
        {
            InitializeComponent();
            txtMessage.KeyDown += TxtMessage_KeyDown;
            refreshTimer.Interval = 3000; // 3 másodpercenként frissít
            refreshTimer.Tick += refreshTimer_Tick;
            refreshTimer.Start();
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMessage.Text)) return;

            var message = new
            {
                Sender = Environment.MachineName,
                Message = txtMessage.Text
            };

            try
            {
                var response = await client.PostAsJsonAsync(serverUrl + "api/chat/send", message);
                if (response.IsSuccessStatusCode)
                {
                    txtMessage.Clear();
                    await LoadMessages();
                }
                else
                {
                    MessageBox.Show("Hiba az üzenet küldésekor.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hálózati hiba: " + ex.Message);
            }
        }

        private async Task LoadMessages()
        {
            try
            {
                var response = await client.GetAsync(serverUrl + "api/chat/get");
                if (response.IsSuccessStatusCode)
                {
                    var messages = await response.Content.ReadFromJsonAsync<List<ChatMessage>>();
                    rtbChat.Clear();

                    foreach (var msg in messages)
                    {
                        rtbChat.AppendText(
                            $"{msg.Timestamp:HH:mm} [{msg.Sender}]:\n" +
                            $"{msg.Message.Replace("\n", Environment.NewLine)}\n\n");

                        lblHeader.Text = $"Partner: {msg.Sender} - Kapcsolat: Online";
                    }
                }
            }
            catch
            {
                lblHeader.Text = "Partner: Ismeretlen - Kapcsolat: Offline";
            }
        }

        private async void TxtMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && e.Control)
            {
                // Ctrl+Enter → új sor
                txtMessage.AppendText(Environment.NewLine);
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Enter)
            {
                // Enter → küldés
                e.SuppressKeyPress = true;
                btnSend.PerformClick();
            }
        }

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            _ = LoadMessages();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void bottomPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }

    public class ChatMessage
    {
        public string Sender { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
