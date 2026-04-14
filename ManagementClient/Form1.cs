using System.Net.Http.Json;
using Microsoft.AspNetCore.SignalR.Client;

namespace ManagementClient
{
    public partial class Form1 : Form
    {
        private readonly HttpClient client = new HttpClient();
        private readonly string serverUrl = AppConfig.AgentsApiUrl;
        private HubConnection? connection;
        private readonly Dictionary<string, ScreenshotForm> openScreenshotForms = new();
        private readonly HashSet<string> activeScreenshotRequests = new();
        private HashSet<string> knownAgents = new();
        private readonly Dictionary<string, MessageForm> openMessageForms = new();
        private string? _pendingNotificationAgent;
        private string? _pendingNotificationAction;


        public Form1()
        {
            InitializeComponent();
            StartSignalR();
            notifyIcon1.BalloonTipClicked += notifyIcon1_BalloonTipClicked;

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
                .WithUrl(AppConfig.HubUrl)
                .WithAutomaticReconnect()
                .Build();
            lblWelcome.Text = $"Üdvözlünk a Remotee applikáció szerver felületén, {AuthSession.FullName}!";

            connection.Reconnecting += error =>
            {
                BeginInvoke(new Action(() =>
                {
                    lblConnectionStatus.Text = "Kapcsolat megszakadt, újrakapcsolódás...";
                }));

                return Task.CompletedTask;
            };

            connection.Reconnected += async connectionId =>
            {
                try
                {
                    if (connection != null)
                    {
                        await connection.InvokeAsync("RegisterManagementClient", AuthSession.Token);

                        BeginInvoke(new Action(() =>
                        {
                            lblConnectionStatus.Text = "Kapcsolat megszakadt, újrakapcsolódás...";
                        }));
                    }
                }
                catch (Exception ex)
                {
                    BeginInvoke(new Action(() =>
                    {
                        lblConnectionStatus.Text = "Újrakapcsolódási hiba";
                        MessageBox.Show("Hiba újrakapcsolódáskor: " + ex.Message);
                    }));
                }
            };

            connection.Closed += error =>
            {
                BeginInvoke(new Action(() =>
                {
                    lblConnectionStatus.Text = "Kapcsolat lezárva";
                }));

                return Task.CompletedTask;
            };

            connection.On<IEnumerable<string>>("AgentListUpdated", agentNames =>
            {
                BeginInvoke(new Action(() =>
                {
                    var newSet = new HashSet<string>(agentNames);

                    var wentOnline = newSet.Except(knownAgents).ToList();
                    var wentOffline = knownAgents.Except(newSet).ToList();

                    lstAgents.Items.Clear();

                    foreach (var name in newSet.OrderBy(x => x))
                    {
                        lstAgents.Items.Add(name);
                    }

                    foreach (var agent in wentOnline)
                    {
                        ShowNotification("Agent online", $"{agent} csatlakozott a szerverhez.");
                    }

                    foreach (var agent in wentOffline)
                    {
                        ShowNotification("Agent offline", $"{agent} lecsatlakozott a szerverrõl.", ToolTipIcon.Warning);
                    }

                    knownAgents = newSet;
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
                    _pendingNotificationAgent = machineName;
                    _pendingNotificationAction = "message";

                    ShowNotification("Új üzenet", $"{machineName} új üzenetet küldött.");

                    if (openMessageForms.TryGetValue(machineName, out var form))
                    {
                        form.AppendIncomingMessage(machineName, message);
                    }
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
                await connection.InvokeAsync("RegisterManagementClient", AuthSession.Token);
                lblConnectionStatus.Text = "Kapcsolódva a szerverhez";
            }
            catch (Exception ex)
            {
                lblConnectionStatus.Text = "SignalR kapcsolati hiba";
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
            OpenMessageForm(agentName);
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
                    $"{AppConfig.AgentsApiUrl}/{selectedAgent}");

                if (info != null)
                {
                    lblMachineName.Text = $"Gépnév: {info.MachineName}";
                    lblOsVersion.Text = $"OS: {info.OsVersion}";
                    lblUserName.Text = $"Felhasználó: {info.UserName}";
                    lblAgentStatus.Text = $"Állapot: {info.Status}";
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
        private async void btnLogout_Click(object sender, EventArgs e)
        {
            try
            {
                using HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("X-Auth-Token", AuthSession.Token);

                await client.PostAsync(AppConfig.AuthLogoutUrl, null);
            }
            catch
            {
            }

            AuthSession.Clear();

            Hide();

            using var loginForm = new LoginForm();
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                Show();
                return;
            }

            Close();
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
        private void notifyIcon1_BalloonTipClicked(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_pendingNotificationAgent) ||
                string.IsNullOrWhiteSpace(_pendingNotificationAction))
            {
                return;
            }

            string agentName = _pendingNotificationAgent;
            string action = _pendingNotificationAction;

            _pendingNotificationAgent = null;
            _pendingNotificationAction = null;

            switch (action)
            {
                case "message":
                    OpenMessageForm(agentName);
                    break;
            }
        }

        private void OpenMessageForm(string agentName)
        {
            if (openMessageForms.ContainsKey(agentName))
            {
                openMessageForms[agentName].BringToFront();
                openMessageForms[agentName].Activate();
                return;
            }

            MessageForm form = new MessageForm(agentName);
            form.FormClosed += (s, args) =>
            {
                openMessageForms.Remove(agentName);
            };

            openMessageForms[agentName] = form;
            form.Show();
        }

    }
}