using System.Net.Http.Json;
using System.Reflection;
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
        public class AgentInfo
        {
            public string MachineName { get; set; } = "";
            public string OsVersion { get; set; } = "";
            public string UserName { get; set; } = "";
            public string Status { get; set; } = "";
        }

        private async void StartSignalR()
        {
            connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7294/agenthub")
                .WithAutomaticReconnect()
                .Build();
            connection.On<string, string>("ReceiveFileResult", (machineName, result) =>
            {
                Invoke(new Action(() =>
                {
                    MessageBox.Show($"{machineName}: {result}");
                }));
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

        private void btnFile_Click(object sender, EventArgs e)
        {
            if (lstAgents.SelectedItem == null)
            {
                MessageBox.Show("Válassz agentet.");
                return;
            }

            string agent = lstAgents.SelectedItem.ToString();

            FileSendForm form = new FileSendForm(agent, connection);
            form.Show();
        }

        private void btnCommand_Click(object sender, EventArgs e)
        {
            if (lstAgents.SelectedItem == null)
            {
                MessageBox.Show("Válassz agentet.");
                return;
            }

            string agentName = lstAgents.SelectedItem.ToString() ?? "";
            CommandForm form = new CommandForm(agentName, connection!);
            form.Show();
        }

        private async void lstAgents_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstAgents.SelectedItem == null)
                return;

            string selectedAgent = lstAgents.SelectedItem.ToString() ?? "";

            try
            {
                var info = await client.GetFromJsonAsync<AgentInfo>(
                    $"https://localhost:7294/api/agents/{selectedAgent}");

                if (info != null)
                {
                    lblMachineName.Text = $"Gépnév: {info.MachineName}";
                    lblOsVersion.Text = $"OS: {info.OsVersion}";
                    lblUserName.Text = $"Felhasználó: {info.UserName}";
                    lblStatus.Text = $"Állapot: {info.Status}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba az agent adatok betöltésekor: " + ex.Message);
            }
        }
    }

}