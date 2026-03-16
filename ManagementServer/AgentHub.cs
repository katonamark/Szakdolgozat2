using System;

using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using ManagementServer;

public class AgentHub : Hub
{
    public static ConcurrentDictionary<string, string> ConnectedAgents = new();
    public static ConcurrentDictionary<string, AgentInfo> AgentInfos = new();

    public async Task RegisterAgent(string machineName, string osVersion, string userName)
    {
        ConnectedAgents[machineName] = Context.ConnectionId;

        AgentInfos[machineName] = new AgentInfo
        {
            MachineName = machineName,
            OsVersion = osVersion,
            UserName = userName,
            Status = "Online"
        };

        await Clients.All.SendAsync("AgentListUpdated", ConnectedAgents.Keys);
    }

    public async Task SendMessageToAgent(string machineName, string message)
    {
        if (ConnectedAgents.TryGetValue(machineName, out var connectionId))
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

        await Clients.All.SendAsync("ReceiveMessageFromAgent", machineName, message);
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var agent = ConnectedAgents.FirstOrDefault(x => x.Value == Context.ConnectionId);

        if (!string.IsNullOrEmpty(agent.Key))
        {
            ConnectedAgents.TryRemove(agent.Key, out _);

            if (AgentInfos.TryGetValue(agent.Key, out var info))
            {
                info.Status = "Offline";
            }

            await Clients.All.SendAsync("AgentListUpdated", ConnectedAgents.Keys);
        }

        await base.OnDisconnectedAsync(exception);
    }

    public async Task SendFileToAgent(string agentName, string fileName, byte[] fileData, string targetPath)
    {
        if (ConnectedAgents.TryGetValue(agentName, out var connectionId))
        {
            await Clients.Client(connectionId)
                .SendAsync("ReceiveFile", fileName, fileData, targetPath);
        }
    }

    public async Task SendCommandToAgent(string agentName, string command)
    {
        if (ConnectedAgents.TryGetValue(agentName, out var connectionId))
        {
            await Clients.Client(connectionId).SendAsync("ReceiveCommand", command);
        }
    }

    public async Task SendCommandResultToManagement(string machineName, string result)
    {
        await Clients.All.SendAsync("ReceiveCommandResult", machineName, result);
    }
}