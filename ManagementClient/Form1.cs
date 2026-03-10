using System.Net.Http.Json;
using Microsoft.AspNetCore.SignalR.Client;

namespace ManagementClient
{
    public partial class Form1 : Form
    {
        private readonly HttpClient client = new HttpClient();
        private readonly string serverUrl = "https://localhost:7294/api/agents";
        private HubConnection? connection;

        public Form1()
        {
            InitializeComponent();
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

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                var agents = await client.GetFromJsonAsync<List<string>>(serverUrl);

                lstAgents.Items.Clear();

                if (agents != null)
                {
                    foreach (var agent in agents)
                    {
                        lstAgents.Items.Add(agent);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba: " + ex.Message);
            }
        }

        private void btnMessage_Click(object sender, EventArgs e)
        {
            if (lstAgents.SelectedItem == null)
            {
                MessageBox.Show("Válassz ki egy agentet.");
                return;
            }

            string agentName = lstAgents.SelectedItem.ToString() ?? "";

            MessageForm form = new MessageForm(agentName);
            form.Show();
        }
    }
}