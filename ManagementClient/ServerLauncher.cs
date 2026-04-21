using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net.Sockets;

namespace ManagementClient;

public static class ServerLauncher
{
    private const string ServerHost = "127.0.0.1";
    private const int ServerPort = 5000;

    public static bool IsServerRunning(int timeoutMs = 500)
    {
        try
        {
            using TcpClient client = new TcpClient();
            var task = client.ConnectAsync(ServerHost, ServerPort);
            return task.Wait(timeoutMs) && client.Connected;
        }
        catch
        {
            return false;
        }
    }

    public static void StartServerIfNeeded()
    {
        if (IsServerRunning())
        {
            return;
        }

        string serverExePath = GetServerExePath();

        if (!File.Exists(serverExePath))
        {
            throw new FileNotFoundException(
                "A ManagementServer.exe nem található. Várt hely: " + serverExePath);
        }

        ProcessStartInfo psi = new ProcessStartInfo
        {
            FileName = serverExePath,
            WorkingDirectory = Path.GetDirectoryName(serverExePath)!,
            UseShellExecute = false,
            CreateNoWindow = true,
            WindowStyle = ProcessWindowStyle.Hidden
        };

        Process.Start(psi);

        WaitForServerStartup();
    }

    private static void WaitForServerStartup(int maxWaitMs = 8000)
    {
        Stopwatch sw = Stopwatch.StartNew();

        while (sw.ElapsedMilliseconds < maxWaitMs)
        {
            if (IsServerRunning())
            {
                return;
            }

            Thread.Sleep(250);
        }

        throw new Exception("A szerver nem indult el időben.");
    }
    private static string GetServerExePath()
    {
#if DEBUG
        return Path.GetFullPath(
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                @"..\..\..\..\ManagementServer\bin\Debug\net8.0\ManagementServer.exe"));
#else
    return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ManagementServer", "ManagementServer.exe");
#endif
    }
}