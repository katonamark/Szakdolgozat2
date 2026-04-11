namespace ManagementClient;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        try
        {
            ServerLauncher.StartServerIfNeeded();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                "A szerver nem indítható el:\n\n" + ex.Message,
                "Szerverindítási hiba",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            return;
        }

        using var loginForm = new LoginForm();
        if (loginForm.ShowDialog() == DialogResult.OK)
        {
            Application.Run(new Form1());
        }
    }
}