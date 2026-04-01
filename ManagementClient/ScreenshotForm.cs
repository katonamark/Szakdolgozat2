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

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            refreshTimer?.Stop();
            refreshTimer?.Dispose();
            base.OnFormClosing(e);
        }

        public ScreenshotForm(Image image, string agentName, HubConnection connection)
        {
            InitializeComponent();

            this.connection = connection;
            this.agentName = agentName;
            this.originalImageWidth = image.Width;
            this.originalImageHeight = image.Height;
            this.KeyPreview = true;
            this.KeyDown += ScreenshotForm_KeyDown;
            this.KeyUp += ScreenshotForm_KeyUp;

            pictureBox1.Image = image;
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

            refreshTimer = new System.Windows.Forms.Timer();
            refreshTimer.Interval = 2000; // 2 másodperc
            refreshTimer.Tick += refreshTimer_Tick;
            refreshTimer.Start();

            pictureBox1.MouseDown += pictureBox1_MouseDown;
            pictureBox1.MouseDoubleClick += pictureBox1_MouseDoubleClick;
            pictureBox1.MouseWheel += pictureBox1_MouseWheel;
            pictureBox1.MouseMove += pictureBox1_MouseMove;
            pictureBox1.Focus();
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

        private async void pictureBox1_MouseDown(object? sender, MouseEventArgs e)
        {
            if (!TryMapToRemoteCoordinates(e.X, e.Y, out int realX, out int realY))
                return;

            string action = e.Button switch
            {
                MouseButtons.Left => "leftclick",
                MouseButtons.Right => "rightclick",
                _ => "move"
            };

            try
            {
                await connection.InvokeAsync("SendMouseActionToAgent", agentName, action, realX, realY, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba egérművelet küldésekor: " + ex.Message);
            }
        }

        private async void pictureBox1_MouseDoubleClick(object? sender, MouseEventArgs e)
        {
            if (!TryMapToRemoteCoordinates(e.X, e.Y, out int realX, out int realY))
                return;

            try
            {
                await connection.InvokeAsync("SendMouseActionToAgent", agentName, "doubleclick", realX, realY, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba dupla kattintás küldésekor: " + ex.Message);
            }
        }

        private async void pictureBox1_MouseWheel(object? sender, MouseEventArgs e)
        {
            if (!TryMapToRemoteCoordinates(e.X, e.Y, out int realX, out int realY))
                return;

            try
            {
                await connection.InvokeAsync("SendMouseActionToAgent", agentName, "wheel", realX, realY, e.Delta);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba görgetés küldésekor: " + ex.Message);
            }
        }

        private async void ScreenshotForm_KeyDown(object? sender, KeyEventArgs e)
        {
            try
            {
                await connection.InvokeAsync(
                    "SendKeyEventToAgent",
                    agentName,
                    (int)e.KeyCode,
                    true,
                    e.Control,
                    e.Alt,
                    e.Shift
                );

                e.SuppressKeyPress = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba KeyDown küldésekor: " + ex.Message);
            }
        }

        private async void ScreenshotForm_KeyUp(object? sender, KeyEventArgs e)
        {
            try
            {
                await connection.InvokeAsync(
                    "SendKeyEventToAgent",
                    agentName,
                    (int)e.KeyCode,
                    false,
                    e.Control,
                    e.Alt,
                    e.Shift
                );

                e.SuppressKeyPress = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba KeyUp küldésekor: " + ex.Message);
            }
        }

        private bool TryMapToRemoteCoordinates(int mouseX, int mouseY, out int realX, out int realY)
        {
            realX = 0;
            realY = 0;

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

            if (mouseX < offsetX || mouseX > offsetX + drawWidth ||
                mouseY < offsetY || mouseY > offsetY + drawHeight)
            {
                return false;
            }

            float relativeX = (float)(mouseX - offsetX) / drawWidth;
            float relativeY = (float)(mouseY - offsetY) / drawHeight;

            realX = (int)(relativeX * originalImageWidth);
            realY = (int)(relativeY * originalImageHeight);

            return true;
        }

        private void pictureBox1_MouseMove(object? sender, MouseEventArgs e)
        {
        }
    }
}