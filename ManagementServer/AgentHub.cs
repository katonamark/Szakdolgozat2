using System;

using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

public class AgentHub : Hub
{
    public static ConcurrentDictionary<string, string> ConnectedAgents = new();

    public async Task RegisterAgent(string machineName)
    {
        ConnectedAgents[machineName] = Context.ConnectionId;
        Console.WriteLine($"Agent csatlakozott: {machineName}");
        await Clients.All.SendAsync("AgentListUpdated", ConnectedAgents.Keys);
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var agent = ConnectedAgents.FirstOrDefault(x => x.Value == Context.ConnectionId);

        if (!string.IsNullOrEmpty(agent.Key))
        {
            ConnectedAgents.TryRemove(agent.Key, out _);
            Console.WriteLine($"Agent lecsatlakozott: {agent.Key}");
            await Clients.All.SendAsync("AgentListUpdated", ConnectedAgents.Keys);
        }

        await base.OnDisconnectedAsync(exception);
    }

    public async Task SendMessageToAgent(string machineName, string message)
    {
        if (ConnectedAgents.TryGetValue(machineName, out var connectionId))
        {
            await Clients.Client(connectionId).SendAsync("ReceiveMessage", message);
        }
    }
}