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

        public FileSendForm(string agentName, HubConnection? hub)
        {
            InitializeComponent();
            targetAgent = agentName;
            connection = hub;
            lblTargetAgent.Text = $"Fájl küldése: {agentName}";
            cmbTargetPath.Items.Add("Desktop");
            cmbTargetPath.Items.Add("Documents");
            cmbTargetPath.Items.Add("Temp");
            cmbTargetPath.Items.Add("Custom");
            cmbTargetPath.SelectedIndex = 0;
            lblStatus.Text = "Állapot: az Agent kész a fogadásra";
            cmbTargetPath.SelectedIndexChanged += cmbTargetPath_SelectedIndexChanged;
            txtTargetPath.Enabled = false;
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
                MessageBox.Show("Válassz fájlt.");
                return;
            }

            string targetPath = GetTargetPath();

            if (string.IsNullOrWhiteSpace(targetPath))
            {
                MessageBox.Show("Add meg a célmappát.");
                return;
            }

            try
            {
                lblStatus.Text = "Állapot: fájl beolvasása...";

                byte[] fileBytes = File.ReadAllBytes(txtFilePath.Text);
                string fileName = Path.GetFileName(txtFilePath.Text);

                lblStatus.Text = "Állapot: fájl küldése...";

                await connection.InvokeAsync(
                    "SendFileToAgent",
                    targetAgent,
                    fileName,
                    fileBytes,
                    targetPath
                );

                lblStatus.Text = "Állapot: fájl elküldve";
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Állapot: hiba";
                MessageBox.Show("Hiba fájlküldéskor: " + ex.Message);
            }
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbTargetPath_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTargetPath.SelectedItem?.ToString() == "Custom")
            {
                txtTargetPath.Enabled = true;
            }
            else
            {
                txtTargetPath.Enabled = false;
            }
        }


        private string GetTargetPath()
        {
            switch (cmbTargetPath.SelectedItem?.ToString())
            {
                case "Desktop":
                    return "Desktop";
                case "Documents":
                    return "Documents";
                case "Temp":
                    return "Temp";
                case "Custom":
                    return txtTargetPath.Text;
                default:
                    return "Desktop";
            }
        }


    }
}