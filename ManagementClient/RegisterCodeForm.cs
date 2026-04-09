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

public partial class RegisterCodeForm : Form
{
    private readonly HttpClient _client = new HttpClient();

    public RegisterCodeForm()
    {
        InitializeComponent();
        txtAdminCode.KeyDown += txtAdminCode_KeyDown;
    }

    private async void btnNext_Click(object sender, EventArgs e)
    {
        try
        {
            var request = new RegisterRequest
            {
                AdminCode = txtAdminCode.Text.Trim()
            };

            var response = await _client.PostAsJsonAsync(
                AppConfig.AuthValidateAdminCodeUrl,
                request);

            var result = await response.Content.ReadFromJsonAsync<AuthResponse>();

            if (result != null && result.Success)
            {
                using var form = new RegisterForm(request.AdminCode);
                form.ShowDialog();
                Close();
                return;
            }

            MessageBox.Show(result?.Message ?? "Érvénytelen admin kód.");
        }
        catch (Exception ex)
        {
            MessageBox.Show("Hiba admin kód ellenőrzésekor: " + ex.Message);
        }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }
    private void txtAdminCode_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            btnNext.PerformClick();
        }
    }
}