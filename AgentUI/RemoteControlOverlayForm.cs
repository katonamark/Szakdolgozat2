using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace AgentUI;

public class RemoteControlOverlayForm : Form
{
    public RemoteControlOverlayForm()
    {
        FormBorderStyle = FormBorderStyle.None;
        StartPosition = FormStartPosition.Manual;
        TopMost = true;
        ShowInTaskbar = false;
        ControlBox = false;
        BackColor = Color.Black;
        Opacity = 0.60;

        var screen = Screen.PrimaryScreen ?? Screen.AllScreens.FirstOrDefault();

        if (screen != null)
        {
            Bounds = screen.Bounds;
        }
        else
        {
            WindowState = FormWindowState.Maximized;
        }

        var label = new Label
        {
            Dock = DockStyle.Fill,
            Text = "Az Ön gépét a szerver jelenleg vezérli.",
            ForeColor = Color.White,
            TextAlign = ContentAlignment.MiddleCenter,
            Font = new Font("Segoe UI", 28, FontStyle.Bold),
            BackColor = Color.Transparent
        };

        Controls.Add(label);
    }

    protected override bool ShowWithoutActivation => true;
}