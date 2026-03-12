using Microsoft.AspNetCore.SignalR.Client;

namespace AgentUI
{
    public partial class Form1 : Form
    {
        private HubConnection? connection;
        private readonly string machineName = Environment.MachineName;

        public Form1()
        {
            InitializeComponent();
            ConnectToServer();
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

            try
            {
                await connection.StartAsync();
                await connection.InvokeAsync("RegisterAgent", machineName);
                lblStatus.Text = $"Kapcsolódva: {machineName}";
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
    }
}