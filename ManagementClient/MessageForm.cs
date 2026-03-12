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
                    $"https://localhost:7294/api/chat/{targetAgent}");

                rtbChatHistory.Clear();

                if (messages != null)
                {
                    foreach (var msg in messages)
                    {
                        rtbChatHistory.AppendText(
                            $"[[{msg.Timestamp:yyyy.MM.dd HH:mm}] {msg.Sender}: {msg.Message}{Environment.NewLine}");
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
                .WithUrl("https://localhost:7294/agenthub")
                .WithAutomaticReconnect()
                .Build();

            connection.On<string, string>("ReceiveMessageFromAgent", (machineName, message) =>
            {
                if (machineName == targetAgent)
                {
                    Invoke(new Action(() =>
                    {
                        rtbChatHistory.AppendText(
                            $"[[{msg.Timestamp:yyyy.MM.dd HH:mm}] {machineName}: {message}{Environment.NewLine}");
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

                rtbChatHistory.AppendText(
                    $"[{msg.Timestamp:yyyy.MM.dd HH:mm}] Management: {messageToSend}{Environment.NewLine}");

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
    }

    public class ChatRecord
    {
        public string Sender { get; set; } = "";
        public string TargetAgent { get; set; } = "";
        public string Message { get; set; } = "";
        public DateTime Timestamp { get; set; }
    }
}