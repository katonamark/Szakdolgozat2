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
    public partial class FileSendForm : Form
    {
        private readonly string targetAgent;
        private HubConnection connection;

        public FileSendForm(string agentName, HubConnection hub)
        {
            InitializeComponent();
            targetAgent = agentName;
            connection = hub;
            lblTargetAgent.Text = $"Fájl küldése: {agentName}";
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text = dlg.FileName;
            }
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFilePath.Text))
            {
                MessageBox.Show("Válaszd ki az elküldendő fájlt.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTargetPath.Text))
            {
                MessageBox.Show("Add meg a célmappát az agent gépen.");
                return;
            }

            byte[] fileBytes = File.ReadAllBytes(txtFilePath.Text);
            string fileName = Path.GetFileName(txtFilePath.Text);
            string targetPath = txtTargetPath.Text;

            await connection.InvokeAsync(
                "SendFileToAgent",
                targetAgent,
                fileName,
                fileBytes,
                targetPath
            );

            MessageBox.Show("Fájl elküldve.");
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}