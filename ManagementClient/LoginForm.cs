using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net.Http.Json;

namespace ManagementClient;

public partial class LoginForm : Form
{
    private readonly HttpClient _client = new HttpClient();

    public LoginForm()
    {
        InitializeComponent();
        txtUsername.KeyDown += txtUsername_KeyDown;
        txtPassword.KeyDown += txtPassword_KeyDown;
    }

    private async void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            var request = new LoginRequest
            {
                Username = txtUsername.Text.Trim(),
                Password = txtPassword.Text
            };

            var response = await _client.PostAsJsonAsync(
                AppConfig.AuthLoginUrl,
                request);

            var result = await response.Content.ReadFromJsonAsync<AuthResponse>();

            if (result != null && result.Success)
            {
                AuthSession.Token = result.Token;
                AuthSession.Username = result.Username;
                AuthSession.FullName = result.FullName;

                DialogResult = DialogResult.OK;
                Close();
                return;
            }

            MessageBox.Show(result?.Message ?? "Sikertelen bejelentkezés.");
        }
        catch (Exception ex)
        {
            MessageBox.Show("Hiba bejelentkezéskor: " + ex.Message);
        }
    }

    private void btnRegister_Click(object sender, EventArgs e)
    {
        using var form = new RegisterCodeForm();
        form.ShowDialog();
    }
    private void txtUsername_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            btnLogin.PerformClick();
        }
    }
    private void txtPassword_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            btnLogin.PerformClick();
        }
    }
}