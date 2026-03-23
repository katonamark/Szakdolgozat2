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
    public partial class ScreenshotForm : Form
    {
        private readonly HubConnection connection;
        private readonly string agentName;
        private readonly int originalImageWidth;
        private readonly int originalImageHeight;
        private System.Windows.Forms.Timer refreshTimer;


        public ScreenshotForm(Image image, string agentName, HubConnection connection)
        {
            InitializeComponent();

            this.connection = connection;
            this.agentName = agentName;
            this.originalImageWidth = image.Width;
            this.originalImageHeight = image.Height;

            pictureBox1.Image = image;
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.MouseClick += pictureBox1_MouseClick;
            refreshTimer = new System.Windows.Forms.Timer();
            refreshTimer.Interval = 2000; // 2 másodperc
            refreshTimer.Tick += refreshTimer_Tick;
            refreshTimer.Start();
        }

        private async void pictureBox1_MouseClick(object? sender, MouseEventArgs e)
        {
            if (pictureBox1.Image == null)
                return;

            int pbWidth = pictureBox1.ClientSize.Width;
            int pbHeight = pictureBox1.ClientSize.Height;

            float imageAspect = (float)originalImageWidth / originalImageHeight;
            float boxAspect = (float)pbWidth / pbHeight;

            int drawWidth, drawHeight, offsetX, offsetY;

            if (imageAspect > boxAspect)
            {
                drawWidth = pbWidth;
                drawHeight = (int)(pbWidth / imageAspect);
                offsetX = 0;
                offsetY = (pbHeight - drawHeight) / 2;
            }
            else
            {
                drawHeight = pbHeight;
                drawWidth = (int)(pbHeight * imageAspect);
                offsetY = 0;
                offsetX = (pbWidth - drawWidth) / 2;
            }

            if (e.X < offsetX || e.X > offsetX + drawWidth ||
                e.Y < offsetY || e.Y > offsetY + drawHeight)
            {
                return;
            }

            float relativeX = (float)(e.X - offsetX) / drawWidth;
            float relativeY = (float)(e.Y - offsetY) / drawHeight;

            int realX = (int)(relativeX * originalImageWidth);
            int realY = (int)(relativeY * originalImageHeight);

            try
            {
                await connection.InvokeAsync("SendMouseClickToAgent", agentName, realX, realY);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba kattintás küldésekor: " + ex.Message);
            }
        }
        private async void refreshTimer_Tick(object? sender, EventArgs e)
        {
            try
            {
                await connection.InvokeAsync("RequestLiveScreenshot", agentName);
            }
            catch
            {
            }
        }
        public void UpdateScreenshot(Image newImage)
        {
            if (pictureBox1.Image != null)
            {
                pictureBox1.Image.Dispose();
            }

            pictureBox1.Image = newImage;
        }
    }
}