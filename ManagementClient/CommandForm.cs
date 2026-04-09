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


namespace ManagementClient
{
    public partial class CommandForm : Form
    {
        private readonly string targetAgent;
        private readonly HubConnection connection;

        public CommandForm(string agentName, HubConnection hub)
        {
            InitializeComponent();
            targetAgent = agentName;
            connection = hub;
            txtCommand.AcceptsReturn = true;
            txtCommand.KeyDown += txtCommand_KeyDown;
            lblTargetAgent.Text = $"Parancsfuttatás: {agentName}";
            StartListening();
        }

        private void StartListening()
        {
            connection.On<string, string>("ReceiveCommandResult", (machineName, result) =>
            {
                if (machineName == targetAgent)
                {
                    Invoke(new Action(() =>
                    {
                        rtbCommandResult.AppendText(
                            $"[{DateTime.Now:yyyy.MM.dd HH:mm}] {machineName}:{Environment.NewLine}{result}{Environment.NewLine}{Environment.NewLine}");
                    }));
                }
            });
        }

        private async void btnRun_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCommand.Text))
            {
                MessageBox.Show("Adj meg egy parancsot.");
                return;
            }

            try
            {
                await connection.InvokeAsync("SendCommandToAgent", targetAgent, txtCommand.Text);
                rtbCommandResult.AppendText(
                    $"[{DateTime.Now:yyyy.MM.dd HH:mm}] Küldött parancs: {txtCommand.Text}{Environment.NewLine}");
                txtCommand.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba parancsküldéskor: " + ex.Message);
            }
        }
        private void txtCommand_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnRun.PerformClick();
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            rtbCommandResult.Clear();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}