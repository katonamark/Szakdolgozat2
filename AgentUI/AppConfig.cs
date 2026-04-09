using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentUI;

public static class AppConfig
{
    public const string ServerBaseUrl = "https://localhost:7294";
    public const string HubUrl = ServerBaseUrl + "/agenthub";
    public const string ChatAgentApiBaseUrl = ServerBaseUrl + "/api/chat/agent/";
}