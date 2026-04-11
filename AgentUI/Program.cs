using System.Threading;

namespace AgentUI;

internal static class Program
{
    private static Mutex? _singleInstanceMutex;

    [STAThread]
    static void Main()
    {
        AppConfig.Load();
        const string mutexName = "RemoteeAgent_SingleInstance";

        bool createdNew;
        _singleInstanceMutex = new Mutex(true, mutexName, out createdNew);

        if (!createdNew)
        {
            return;
        }

        ApplicationConfiguration.Initialize();
        Application.Run(new ClientUI());

        _singleInstanceMutex.ReleaseMutex();
        _singleInstanceMutex.Dispose();
    }
}