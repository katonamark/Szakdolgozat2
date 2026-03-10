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

namespace ManagementClient
{
    public partial class MessageForm : Form
    {
        private readonly string targetAgent;
        private HubConnection? connection;

        public MessageForm(string agentName)
        {
            InitializeComponent();
            targetAgent = agentName;
            lblTargetAgent.Text = $"Üzenet küldése: {agentName}";
            StartSignalR();
        }

        private async void StartSignalR()
        {
            connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7294/agenthub")
                .WithAutomaticReconnect()
                .Build();

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
            if (string.IsNullOrWhiteSpace(rtbMessage.Text))
            {
                MessageBox.Show("Írj be egy üzenetet.");
                return;
            }

            if (connection == null)
            {
                MessageBox.Show("Nincs kapcsolat a szerverrel.");
                return;
            }

            try
            {
                await connection.InvokeAsync("SendMessageToAgent", targetAgent, rtbMessage.Text);
                MessageBox.Show("Üzenet elküldve.");
                rtbMessage.Clear();
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
}