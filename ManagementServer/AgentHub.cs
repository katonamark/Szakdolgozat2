using System.Collections.Concurrent;
using Microsoft.AspNetCore.SignalR;
using ManagementServer.Services;

namespace ManagementServer
{
    public class AgentHub : Hub
    {
        private const string SharedSecret = "my-szakdolgozat-super-secret-password-2026-mark";
        private const string ManagementGroup = "management";

        private readonly AgentRegistry _registry;
        private readonly ILogger<AgentHub> _logger;
        private readonly AuthService _authService;

        private static readonly ConcurrentDictionary<string, HashSet<string>> RemoteControlWatchers = new();
        private static readonly object RemoteControlLock = new();

        public AgentHub(AgentRegistry registry, ILogger<AgentHub> logger, AuthService authService)
        {
            _registry = registry;
            _logger = logger;
            _authService = authService;
        }

        public async Task RegisterManagementClient(string authToken)
        {
            if (!_authService.ValidateToken(authToken))
                throw new HubException("Érvénytelen vagy lejárt bejelentkezési token.");

            _registry.AddManagement(Context.ConnectionId);
            await Groups.AddToGroupAsync(Context.ConnectionId, ManagementGroup);

            await Clients.Caller.SendAsync("RegistrationSucceeded", "management");
            await Clients.Group(ManagementGroup).SendAsync("AgentListUpdated", _registry.AgentNames);
            await Clients.Group(ManagementGroup).SendAsync("AgentInfosUpdated", _registry.AgentInfos);
        }

        public async Task RegisterAgent(string machineName, string osVersion, string userName, string sharedSecret)
        {
            if (sharedSecret != SharedSecret)
                throw new HubException("Érvénytelen agent hitelesítő kulcs.");

            _registry.AddOrUpdateAgent(machineName, Context.ConnectionId, osVersion, userName);

            _logger.LogInformation(
                "Agent connected: {MachineName} ({UserName}, {OsVersion})",
                machineName, userName, osVersion);

            await Clients.Caller.SendAsync("RegistrationSucceeded", "agent");
            await Clients.Group(ManagementGroup).SendAsync("AgentListUpdated", _registry.AgentNames);
            await Clients.Group(ManagementGroup).SendAsync("AgentInfosUpdated", _registry.AgentInfos);
        }

        public async Task SendMessageToAgent(string machineName, string message)
        {
            if (_registry.TryGetAgentConnection(machineName, out var connectionId))
            {
                ChatStorage.AddMessage(new ChatRecord
                {
                    Sender = "Management",
                    TargetAgent = machineName,
                    Message = message,
                    Timestamp = DateTime.Now
                });

                await Clients.Client(connectionId).SendAsync("ReceiveMessage", message);
            }
        }

        public async Task SendMessageToManagement(string machineName, string message)
        {
            ChatStorage.AddMessage(new ChatRecord
            {
                Sender = machineName,
                TargetAgent = machineName,
                Message = message,
                Timestamp = DateTime.Now
            });

            await Clients.Group(ManagementGroup).SendAsync("ReceiveMessageFromAgent", machineName, message);
        }

        public async Task SendFileToAgent(string agentName, string fileName, byte[] fileData, string targetPath)
        {
            if (_registry.TryGetAgentConnection(agentName, out var connectionId))
            {
                await Clients.Client(connectionId).SendAsync("ReceiveFile", fileName, fileData, targetPath);
            }
        }

        public async Task SendCommandToAgent(string agentName, string command)
        {
            if (_registry.TryGetAgentConnection(agentName, out var connectionId))
            {
                await Clients.Client(connectionId).SendAsync("ReceiveCommand", command);
            }
        }

        public async Task SendCommandResultToManagement(string machineName, string result)
        {
            await Clients.Group(ManagementGroup).SendAsync("ReceiveCommandResult", machineName, result);
        }

        public async Task SendFileResultToManagement(string machineName, string result)
        {
            await Clients.Group(ManagementGroup).SendAsync("ReceiveFileResult", machineName, result);
        }

        public async Task RequestScreenshot(string agentName)
        {
            if (_registry.TryGetAgentConnection(agentName, out var connectionId))
            {
                await Clients.Client(connectionId).SendAsync("TakeScreenshot");
            }
        }

        public async Task RequestLiveScreenshot(string agentName)
        {
            if (_registry.TryGetAgentConnection(agentName, out var connectionId))
            {
                await Clients.Client(connectionId).SendAsync("TakeScreenshot");
            }
        }

        public async Task SendScreenshotToManagement(string machineName, byte[] imageBytes)
        {
            await Clients.Group(ManagementGroup).SendAsync("ReceiveScreenshot", machineName, imageBytes);
        }

        public async Task SendMouseActionToAgent(string agentName, string action, int x, int y, int delta = 0)
        {
            if (_registry.TryGetAgentConnection(agentName, out var connectionId))
            {
                await Clients.Client(connectionId).SendAsync("ReceiveMouseAction", action, x, y, delta);
            }
        }

        public async Task SendKeyEventToAgent(string agentName, int keyCode, bool keyDown, bool ctrl, bool alt, bool shift)
        {
            if (_registry.TryGetAgentConnection(agentName, out var connectionId))
            {
                await Clients.Client(connectionId).SendAsync("ReceiveKeyEvent", agentName, keyCode, keyDown, ctrl, alt, shift);
            }
        }

        public async Task StartRemoteControl(string agentName)
        {
            bool firstViewer = false;

            lock (RemoteControlLock)
            {
                var watchers = RemoteControlWatchers.GetOrAdd(agentName, _ => new HashSet<string>());

                if (watchers.Add(Context.ConnectionId) && watchers.Count == 1)
                {
                    firstViewer = true;
                }
            }

            if (firstViewer && _registry.TryGetAgentConnection(agentName, out var connectionId))
            {
                await Clients.Client(connectionId).SendAsync("RemoteControlSessionChanged", true);
            }
        }

        public async Task StopRemoteControl(string agentName)
        {
            bool noMoreViewers = false;

            lock (RemoteControlLock)
            {
                if (RemoteControlWatchers.TryGetValue(agentName, out var watchers))
                {
                    watchers.Remove(Context.ConnectionId);

                    if (watchers.Count == 0)
                    {
                        RemoteControlWatchers.TryRemove(agentName, out _);
                        noMoreViewers = true;
                    }
                }
            }

            if (noMoreViewers && _registry.TryGetAgentConnection(agentName, out var connectionId))
            {
                await Clients.Client(connectionId).SendAsync("RemoteControlSessionChanged", false);
            }
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            if (_registry.IsManagementConnection(Context.ConnectionId))
            {
                _registry.RemoveManagement(Context.ConnectionId);
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, ManagementGroup);

                var agentsToStop = new List<string>();

                lock (RemoteControlLock)
                {
                    foreach (var pair in RemoteControlWatchers.ToList())
                    {
                        if (pair.Value.Remove(Context.ConnectionId) && pair.Value.Count == 0)
                        {
                            RemoteControlWatchers.TryRemove(pair.Key, out _);
                            agentsToStop.Add(pair.Key);
                        }
                    }
                }

                foreach (var agentName in agentsToStop)
                {
                    if (_registry.TryGetAgentConnection(agentName, out var agentConnectionId))
                    {
                        await Clients.Client(agentConnectionId).SendAsync("RemoteControlSessionChanged", false);
                    }
                }
            }
            else
            {
                _registry.MarkAgentOfflineByConnectionId(Context.ConnectionId);

                await Clients.Group(ManagementGroup).SendAsync("AgentListUpdated", _registry.AgentNames);
                await Clients.Group(ManagementGroup).SendAsync("AgentInfosUpdated", _registry.AgentInfos);
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}
/*using System;
using Microsoft.AspNetCore.SignalR;
using ManagementServer;
using ManagementServer.Services;

namespace ManagementServer;
public class AgentHub : Hub
{
    private const string SharedSecret = "my-szakdolgozat-super-secret-password-2026-mark";
    private const string ManagementGroup = "management";

    private readonly AgentRegistry _registry;
    private readonly ILogger<AgentHub> _logger;
    private readonly AuthService _authService;

    public AgentHub(AgentRegistry registry, ILogger<AgentHub> logger, AuthService authService)
    {
        _registry = registry;
        _logger = logger;
        _authService = authService;
    }

    public async Task RegisterManagementClient(string authToken)
    {
        if (!_authService.ValidateToken(authToken))
        {
            throw new HubException("Érvénytelen vagy lejárt bejelentkezési token.");
        }

        _registry.AddManagement(Context.ConnectionId);
        await Groups.AddToGroupAsync(Context.ConnectionId, ManagementGroup);

        await Clients.Caller.SendAsync("RegistrationSucceeded", "management");
        await Clients.Group(ManagementGroup).SendAsync("AgentListUpdated", _registry.AgentNames);
        await Clients.Group(ManagementGroup).SendAsync("AgentInfosUpdated", _registry.AgentInfos);
    }

    public async Task RegisterAgent(string machineName, string osVersion, string userName, string sharedSecret)
    {
        if (sharedSecret != SharedSecret)
        {
            throw new HubException("Érvénytelen agent hitelesítő kulcs.");
        }

        _registry.AddOrUpdateAgent(machineName, Context.ConnectionId, osVersion, userName);

        _logger.LogInformation("Agent connected: {MachineName} ({UserName}, {OsVersion})", machineName, userName, osVersion);

        await Clients.Caller.SendAsync("RegistrationSucceeded", "agent");
        await Clients.Group(ManagementGroup).SendAsync("AgentListUpdated", _registry.AgentNames);
        await Clients.Group(ManagementGroup).SendAsync("AgentInfosUpdated", _registry.AgentInfos);
    }

    public async Task SendMessageToAgent(string machineName, string message)
    {
        if (_registry.TryGetAgentConnection(machineName, out var connectionId))
        {
            ChatStorage.AddMessage(new ChatRecord
            {
                Sender = "Management",
                TargetAgent = machineName,
                Message = message,
                Timestamp = DateTime.Now
            });

            await Clients.Client(connectionId).SendAsync("ReceiveMessage", message);
        }
    }

    public async Task SendMessageToManagement(string machineName, string message)
    {
        ChatStorage.AddMessage(new ChatRecord
        {
            Sender = machineName,
            TargetAgent = machineName,
            Message = message,
            Timestamp = DateTime.Now
        });

        await Clients.Group(ManagementGroup).SendAsync("ReceiveMessageFromAgent", machineName, message);
    }

    public async Task SendFileToAgent(string agentName, string fileName, byte[] fileData, string targetPath)
    {
        if (_registry.TryGetAgentConnection(agentName, out var connectionId))
        {
            await Clients.Client(connectionId).SendAsync("ReceiveFile", fileName, fileData, targetPath);
        }
    }

    public async Task SendCommandToAgent(string agentName, string command)
    {
        if (_registry.TryGetAgentConnection(agentName, out var connectionId))
        {
            await Clients.Client(connectionId).SendAsync("ReceiveCommand", command);
        }
    }

    public async Task SendCommandResultToManagement(string machineName, string result)
    {
        await Clients.Group(ManagementGroup).SendAsync("ReceiveCommandResult", machineName, result);
    }

    public async Task SendFileResultToManagement(string machineName, string result)
    {
        await Clients.Group(ManagementGroup).SendAsync("ReceiveFileResult", machineName, result);
    }

    public async Task RequestScreenshot(string agentName)
    {
        if (_registry.TryGetAgentConnection(agentName, out var connectionId))
        {
            await Clients.Client(connectionId).SendAsync("TakeScreenshot");
        }
    }

    public async Task RequestLiveScreenshot(string agentName)
    {
        if (_registry.TryGetAgentConnection(agentName, out var connectionId))
        {
            await Clients.Client(connectionId).SendAsync("TakeScreenshot");
        }
    }

    public async Task SendScreenshotToManagement(string machineName, byte[] imageBytes)
    {
        await Clients.Group(ManagementGroup).SendAsync("ReceiveScreenshot", machineName, imageBytes);
    }

    public async Task SendMouseActionToAgent(string agentName, string action, int x, int y, int delta = 0)
    {
        if (_registry.TryGetAgentConnection(agentName, out var connectionId))
        {
            await Clients.Client(connectionId).SendAsync("ReceiveMouseAction", action, x, y, delta);
        }
    }

    public async Task SendKeyEventToAgent(string agentName, int keyCode, bool keyDown, bool ctrl, bool alt, bool shift)
    {
        if (_registry.TryGetAgentConnection(agentName, out var connectionId))
        {
            await Clients.Client(connectionId).SendAsync("ReceiveKeyEvent", keyCode, keyDown, ctrl, alt, shift);
        }
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        if (_registry.IsManagementConnection(Context.ConnectionId))
        {
            _registry.RemoveManagement(Context.ConnectionId);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, ManagementGroup);
        }
        else
        {
            _registry.MarkAgentOfflineByConnectionId(Context.ConnectionId);
            await Clients.Group(ManagementGroup).SendAsync("AgentListUpdated", _registry.AgentNames);
            await Clients.Group(ManagementGroup).SendAsync("AgentInfosUpdated", _registry.AgentInfos);
        }

        await base.OnDisconnectedAsync(exception);
    }
    public async Task StartRemoteControl(string agentName)
    {
        if (_registry.TryGetAgentConnection(agentName, out var connectionId))
        {
            await Clients.Client(connectionId).SendAsync("RemoteControlStarted");
        }
    }

    public async Task StopRemoteControl(string agentName)
    {
        if (_registry.TryGetAgentConnection(agentName, out var connectionId))
        {
            await Clients.Client(connectionId).SendAsync("RemoteControlStopped");
        }
    }

}*/