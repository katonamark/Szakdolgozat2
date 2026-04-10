using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.SignalR.Protocol;
using System.Net.Http.Json;

namespace ManagementClient
{
    public partial class MessageForm : Form
    {
        private readonly string targetAgent;
        private HubConnection? connection;
        private readonly HttpClient client = new HttpClient();

        public MessageForm(string agentName)
        {
            InitializeComponent();
            txtNewMessage.KeyDown += txtNewMessage_KeyDown;
            txtNewMessage.Multiline = true;
            txtNewMessage.AcceptsReturn = true;
            targetAgent = agentName;
            lblTargetAgent.Text = $"Beszélgetés: {agentName}";
            LoadConversation();
            StartSignalR();
        }

        private async void LoadConversation()
        {
            try
            {
                var messages = await client.GetFromJsonAsync<List<ChatRecord>>(
                    $"{AppConfig.ChatApiBaseUrl}{targetAgent}");

                rtbChatHistory.Clear();

                if (messages != null)
                {
                    foreach (var msg in messages.OrderBy(m => m.Timestamp))
                    {
                        AppendChatLine($"[{msg.Timestamp:yyyy.MM.dd HH:mm}] {msg.Sender}: {msg.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba a beszélgetés betöltésekor: " + ex.Message);
            }
        }

        private async void StartSignalR()
        {
            connection = new HubConnectionBuilder()
                .WithUrl(AppConfig.HubUrl)
                .WithAutomaticReconnect()
                .Build();

            connection.On<string, string>("ReceiveMessageFromAgent", (machineName, message) =>
            {
                if (machineName == targetAgent)
                {
                    Invoke(new Action(() =>
                    {
                        AppendChatLine($"[{DateTime.Now:yyyy.MM.dd HH:mm}] {machineName}: {message}");
                    }));
                }
            });

            try
            {
                await connection.StartAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("SignalR hiba: " + ex.Message);
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
                await connection.InvokeAsync("SendMessageToAgent", targetAgent, messageToSend);

                AppendChatLine($"[{DateTime.Now:yyyy.MM.dd HH:mm}] Management: {messageToSend}");

                txtNewMessage.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba üzenetküldéskor: " + ex.Message);
            }
        }

        private void MessageForm_Load(object sender, EventArgs e)
        {
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
        public void AppendIncomingMessage(string senderName, string message)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => AppendIncomingMessage(senderName, message)));
                return;
            }

            AppendChatLine($"[{DateTime.Now:yyyy.MM.dd HH:mm}] {senderName}: {message}");
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

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    public class ChatRecord
    {
        public string Sender { get; set; } = "";
        public string TargetAgent { get; set; } = "";
        public string Message { get; set; } = "";
        public DateTime Timestamp { get; set; }
    }
}