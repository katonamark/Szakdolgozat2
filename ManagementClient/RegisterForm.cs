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

public partial class RegisterForm : Form
{
    private readonly HttpClient _client = new HttpClient();
    private readonly string _adminCode;

    public RegisterForm(string adminCode)
    {
        InitializeComponent();
        _adminCode = adminCode;
    }

    private async void btnRegister_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtFullName.Text))
        {
            MessageBox.Show("Add meg a teljes nevet.");
            return;
        }

        if (string.IsNullOrWhiteSpace(txtUsername.Text))
        {
            MessageBox.Show("Add meg a felhasználónevet.");
            return;
        }

        if (string.IsNullOrWhiteSpace(txtPassword.Text))
        {
            MessageBox.Show("Add meg a jelszót.");
            return;
        }

        if (txtPassword.Text != txtPasswordAgain.Text)
        {
            MessageBox.Show("A két jelszó nem egyezik.");
            return;
        }

        try
        {
            var request = new RegisterRequest
            {
                AdminCode = _adminCode,
                FullName = txtFullName.Text.Trim(),
                Username = txtUsername.Text.Trim(),
                Password = txtPassword.Text
            };

            var response = await _client.PostAsJsonAsync(
                AppConfig.AuthRegisterUrl,
                request);

            var result = await response.Content.ReadFromJsonAsync<AuthResponse>();

            if (result != null && result.Success)
            {
                MessageBox.Show("Sikeres regisztráció.");
                Close();
                return;
            }

            MessageBox.Show(result?.Message ?? "Sikertelen regisztráció.");
        }
        catch (Exception ex)
        {
            MessageBox.Show("Hiba regisztrációkor: " + ex.Message);
        }
    }

     private void btnBack_Click(object sender, EventArgs e)
    {
        Close();
    }
}
