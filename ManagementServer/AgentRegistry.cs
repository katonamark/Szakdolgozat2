using System.Collections.Concurrent;

namespace ManagementServer;

public sealed class AgentRegistry
{
    private readonly ConcurrentDictionary<string, string> _agentConnections = new();
    private readonly ConcurrentDictionary<string, AgentInfo> _agentInfos = new();
    private readonly ConcurrentDictionary<string, byte> _managementConnections = new();

    public IReadOnlyCollection<string> AgentNames => _agentConnections.Keys.ToList().AsReadOnly();
    public IReadOnlyCollection<AgentInfo> AgentInfos => _agentInfos.Values.ToList().AsReadOnly();
    public IReadOnlyCollection<string> ManagementConnectionIds => _managementConnections.Keys.ToList().AsReadOnly();

    public void AddManagement(string connectionId)
    {
        _managementConnections[connectionId] = 0;
    }

    public void RemoveManagement(string connectionId)
    {
        _managementConnections.TryRemove(connectionId, out _);
    }

    public void AddOrUpdateAgent(string machineName, string connectionId, string osVersion, string userName)
    {
        _agentConnections[machineName] = connectionId;
        _agentInfos[machineName] = new AgentInfo
        {
            MachineName = machineName,
            OsVersion = osVersion,
            UserName = userName,
            Status = "Online"
        };
    }

    public bool TryGetAgentConnection(string machineName, out string connectionId)
        => _agentConnections.TryGetValue(machineName, out connectionId!);

    public void MarkAgentOfflineByConnectionId(string connectionId)
    {
        var pair = _agentConnections.FirstOrDefault(x => x.Value == connectionId);
        if (string.IsNullOrWhiteSpace(pair.Key))
        {
            return;
        }

        _agentConnections.TryRemove(pair.Key, out _);

        if (_agentInfos.TryGetValue(pair.Key, out var info))
        {
            info.Status = "Offline";
        }
    }

    public bool IsManagementConnection(string connectionId)
        => _managementConnections.ContainsKey(connectionId);
}