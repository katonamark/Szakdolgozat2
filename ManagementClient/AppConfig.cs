using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementClient;

public static class AppConfig
{
    public const string ServerBaseUrl = "http://127.0.0.1:5000";

    public const string HubUrl = ServerBaseUrl + "/agenthub";
    public const string ChatApiBaseUrl = ServerBaseUrl + "/api/chat/";
    public const string AgentsApiUrl = ServerBaseUrl + "/api/agents";
    public const string AuthLoginUrl = ServerBaseUrl + "/api/auth/login";
    public const string AuthRegisterUrl = ServerBaseUrl + "/api/auth/register";
    public const string AuthValidateAdminCodeUrl = ServerBaseUrl + "/api/auth/validate-admin-code";
    public const string AuthLogoutUrl = ServerBaseUrl + "/api/auth/logout";
}