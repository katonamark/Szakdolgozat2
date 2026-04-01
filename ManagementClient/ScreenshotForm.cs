using Microsoft.AspNetCore.SignalR.Client;

namespace ManagementClient;

public partial class ScreenshotForm : Form
{
    private readonly HubConnection _connection;
    private readonly string _agentName;
    private readonly System.Windows.Forms.Timer _refreshTimer;

    private int _currentImageWidth;
    private int _currentImageHeight;
    private bool _requestInFlight;

    public ScreenshotForm(Image image, string agentName, HubConnection connection)
    {
        InitializeComponent();

        _connection = connection;
        _agentName = agentName;

        _currentImageWidth = image.Width;
        _currentImageHeight = image.Height;

        KeyPreview = true;
        KeyDown += ScreenshotForm_KeyDown;
        KeyUp += ScreenshotForm_KeyUp;

        pictureBox1.Image = image;
        pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
        pictureBox1.TabStop = true;

        pictureBox1.MouseUp += pictureBox1_MouseUp;
        pictureBox1.MouseDoubleClick += pictureBox1_MouseDoubleClick;
        pictureBox1.MouseWheel += pictureBox1_MouseWheel;
        pictureBox1.MouseEnter += (_, _) => pictureBox1.Focus();
        pictureBox1.Click += (_, _) => pictureBox1.Focus();

        _refreshTimer = new System.Windows.Forms.Timer();
        _refreshTimer.Interval = 1000;
        _refreshTimer.Tick += refreshTimer_Tick;
        _refreshTimer.Start();
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        _refreshTimer.Stop();
        _refreshTimer.Dispose();

        if (pictureBox1.Image != null)
        {
            pictureBox1.Image.Dispose();
            pictureBox1.Image = null;
        }

        base.OnFormClosing(e);
    }

    private async void refreshTimer_Tick(object? sender, EventArgs e)
    {
        if (_requestInFlight)
            return;

        if (_connection.State != HubConnectionState.Connected)
            return;

        try
        {
            _requestInFlight = true;
            await _connection.InvokeAsync("RequestLiveScreenshot", _agentName);
        }
        catch
        {
        }
        finally
        {
            _requestInFlight = false;
        }
    }

    public void UpdateScreenshot(Image newImage)
    {
        var old = pictureBox1.Image;
        pictureBox1.Image = newImage;

        _currentImageWidth = newImage.Width;
        _currentImageHeight = newImage.Height;

        old?.Dispose();
    }

    private async void pictureBox1_MouseUp(object? sender, MouseEventArgs e)
    {
        pictureBox1.Focus();

        if (_connection.State != HubConnectionState.Connected)
            return;

        if (!TryMapToRemoteCoordinates(e.X, e.Y, out int realX, out int realY))
            return;

        string? action = e.Button switch
        {
            MouseButtons.Left => "leftclick",
            MouseButtons.Right => "rightclick",
            _ => null
        };

        if (action is null)
            return;

        try
        {
            await _connection.InvokeAsync("SendMouseActionToAgent", _agentName, action, realX, realY, 0);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Hiba egérművelet küldésekor: " + ex.Message);
        }
    }

    private async void pictureBox1_MouseDoubleClick(object? sender, MouseEventArgs e)
    {
        pictureBox1.Focus();

        if (_connection.State != HubConnectionState.Connected)
            return;

        if (!TryMapToRemoteCoordinates(e.X, e.Y, out int realX, out int realY))
            return;

        try
        {
            await _connection.InvokeAsync("SendMouseActionToAgent", _agentName, "doubleclick", realX, realY, 0);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Hiba dupla kattintás küldésekor: " + ex.Message);
        }
    }

    private async void pictureBox1_MouseWheel(object? sender, MouseEventArgs e)
    {
        pictureBox1.Focus();

        if (_connection.State != HubConnectionState.Connected)
            return;

        if (!TryMapToRemoteCoordinates(e.X, e.Y, out int realX, out int realY))
            return;

        try
        {
            await _connection.InvokeAsync("SendMouseActionToAgent", _agentName, "wheel", realX, realY, e.Delta);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Hiba görgetés küldésekor: " + ex.Message);
        }
    }

    private async void ScreenshotForm_KeyDown(object? sender, KeyEventArgs e)
    {
        if (_connection.State != HubConnectionState.Connected)
            return;

        try
        {
            await _connection.InvokeAsync(
                "SendKeyEventToAgent",
                _agentName,
                (int)e.KeyCode,
                true,
                e.Control,
                e.Alt,
                e.Shift);

            e.SuppressKeyPress = true;
        }
        catch (Exception ex)
        {
            MessageBox.Show("Hiba KeyDown küldésekor: " + ex.Message);
        }
    }

    private async void ScreenshotForm_KeyUp(object? sender, KeyEventArgs e)
    {
        if (_connection.State != HubConnectionState.Connected)
            return;

        try
        {
            await _connection.InvokeAsync(
                "SendKeyEventToAgent",
                _agentName,
                (int)e.KeyCode,
                false,
                e.Control,
                e.Alt,
                e.Shift);

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

        if (pbWidth <= 0 || pbHeight <= 0 || _currentImageWidth <= 0 || _currentImageHeight <= 0)
            return false;

        float imageAspect = (float)_currentImageWidth / _currentImageHeight;
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

        if (mouseX < offsetX || mouseX > offsetX + drawWidth || mouseY < offsetY || mouseY > offsetY + drawHeight)
            return false;

        float relativeX = (float)(mouseX - offsetX) / drawWidth;
        float relativeY = (float)(mouseY - offsetY) / drawHeight;

        realX = (int)(relativeX * _currentImageWidth);
        realY = (int)(relativeY * _currentImageHeight);

        realX = Math.Clamp(realX, 0, _currentImageWidth - 1);
        realY = Math.Clamp(realY, 0, _currentImageHeight - 1);

        return true;
    }
}