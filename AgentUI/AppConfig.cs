using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentUI;

public static class AppConfig
{
    public const string ServerBaseUrl = "http://127.0.0.1:5000";
    public const string HubUrl = ServerBaseUrl + "/agenthub";
    public const string ChatAgentApiBaseUrl = ServerBaseUrl + "/api/chat/agent/";
}