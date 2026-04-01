using System.Net.Http.Json;
using Microsoft.AspNetCore.SignalR.Client;

namespace ManagementClient
{
    public partial class Form1 : Form
    {
        private readonly HttpClient client = new HttpClient();
        private readonly string serverUrl = "https://localhost:7294/api/agents";
        private HubConnection? connection;

        private const string SharedSecret = "my-szakdolgozat-super-secret-password-2026-mark";

        private readonly Dictionary<string, ScreenshotForm> openScreenshotForms = new();
        private readonly HashSet<string> activeScreenshotRequests = new();

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

            connection.Reconnecting += error =>
            {
                BeginInvoke(new Action(() =>
                {
                    lblStatus.Text = "Kapcsolat megszakadt, újrakapcsolódás...";
                }));

                return Task.CompletedTask;
            };

            connection.Reconnected += async connectionId =>
            {
                try
                {
                    if (connection != null)
                    {
                        await connection.InvokeAsync("RegisterManagementClient", SharedSecret);

                        BeginInvoke(new Action(() =>
                        {
                            lblStatus.Text = "Újrakapcsolódva a szerverhez";
                        }));
                    }
                }
                catch (Exception ex)
                {
                    BeginInvoke(new Action(() =>
                    {
                        lblStatus.Text = "Újrakapcsolódási hiba";
                        MessageBox.Show("Hiba újrakapcsolódáskor: " + ex.Message);
                    }));
                }
            };

            connection.Closed += error =>
            {
                BeginInvoke(new Action(() =>
                {
                    lblStatus.Text = "Kapcsolat lezárva";
                }));

                return Task.CompletedTask;
            };

            connection.On<IEnumerable<string>>("AgentListUpdated", agentNames =>
            {
                BeginInvoke(new Action(() =>
                {
                    lstAgents.Items.Clear();

                    foreach (var name in agentNames.OrderBy(x => x))
                    {
                        lstAgents.Items.Add(name);
                    }
                }));
            });

            connection.On<string, string>("ReceiveFileResult", (machineName, result) =>
            {
                BeginInvoke(new Action(() =>
                {
                    MessageBox.Show($"{machineName}: {result}");
                }));
            });

            connection.On<string, string>("ReceiveCommandResult", (machineName, result) =>
            {
                BeginInvoke(new Action(() =>
                {
                    MessageBox.Show($"{machineName} parancs eredménye:\n\n{result}");
                }));
            });

            connection.On<string, string>("ReceiveMessageFromAgent", (machineName, message) =>
            {
                BeginInvoke(new Action(() =>
                {
                    MessageBox.Show($"{machineName} üzenete:\n\n{message}");
                }));
            });

            connection.On<string, byte[]>("ReceiveScreenshot", (machineName, imageBytes) =>
            {
                BeginInvoke(new Action(() =>
                {
                    if (!activeScreenshotRequests.Contains(machineName))
                        return;

                    try
                    {
                        using MemoryStream ms = new MemoryStream(imageBytes);
                        using Image img = Image.FromStream(ms);
                        Image cloned = (Image)img.Clone();

                        if (openScreenshotForms.ContainsKey(machineName))
                        {
                            openScreenshotForms[machineName].UpdateScreenshot(cloned);
                        }
                        else
                        {
                            if (connection == null)
                            {
                                cloned.Dispose();
                                return;
                            }

                            ScreenshotForm form = new ScreenshotForm(cloned, machineName, connection);
                            form.Text = $"Képernyõkép - {machineName}";

                            form.FormClosed += (s, e) =>
                            {
                                openScreenshotForms.Remove(machineName);
                                activeScreenshotRequests.Remove(machineName);
                            };

                            openScreenshotForms[machineName] = form;
                            form.Show();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Hiba screenshot feldolgozásakor: " + ex.Message);
                    }
                }));
            });

            try
            {
                await connection.StartAsync();
                await connection.InvokeAsync("RegisterManagementClient", SharedSecret);
                lblStatus.Text = "Kapcsolódva a szerverhez";
            }
            catch (Exception ex)
            {
                lblStatus.Text = "SignalR kapcsolati hiba";
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
                    foreach (var agent in agents.OrderBy(x => x))
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

            if (connection == null)
            {
                MessageBox.Show("Nincs kapcsolat a szerverrel.");
                return;
            }

            string agent = lstAgents.SelectedItem.ToString() ?? "";
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

            if (connection == null)
            {
                MessageBox.Show("Nincs kapcsolat a szerverrel.");
                return;
            }

            string agentName = lstAgents.SelectedItem.ToString() ?? "";
            CommandForm form = new CommandForm(agentName, connection);
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

        private async void btnScreenshot_Click(object sender, EventArgs e)
        {
            if (lstAgents.SelectedItem == null)
            {
                MessageBox.Show("Válassz agentet.");
                return;
            }

            if (connection == null || connection.State != HubConnectionState.Connected)
            {
                MessageBox.Show("Nincs aktív kapcsolat a szerverrel.");
                return;
            }

            string agentName = lstAgents.SelectedItem.ToString() ?? "";

            activeScreenshotRequests.Add(agentName);

            try
            {
                await connection.InvokeAsync("RequestScreenshot", agentName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba képernyõkép kéréskor: " + ex.Message);
            }
        }
    }
}