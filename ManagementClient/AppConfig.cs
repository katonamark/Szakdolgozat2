using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace ManagementClient;

public static class AppConfig
{
    public static string ServerBaseUrl { get; private set; } = "http://127.0.0.1:5000";
    public static string HubUrl => ServerBaseUrl + "/agenthub";
    public static string AgentsApiUrl => ServerBaseUrl + "/api/agents";
    public static string ChatApiBaseUrl => ServerBaseUrl + "/api/chat/";
    public static string AuthLoginUrl => ServerBaseUrl + "/api/auth/login";
    public static string AuthLogoutUrl => ServerBaseUrl + "/api/auth/logout";
    public static string AuthRegisterUrl => ServerBaseUrl + "/api/auth/register";
    public static string AuthValidateAdminCodeUrl => ServerBaseUrl + "/api/auth/validate-admin-code";

    public static void Load()
    {
        try
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");

            if (!File.Exists(path))
                return;

            string json = File.ReadAllText(path);

            var config = JsonSerializer.Deserialize<ConfigModel>(json);

            if (!string.IsNullOrWhiteSpace(config?.ServerUrl))
            {
                ServerBaseUrl = config.ServerUrl;
            }
        }
        catch
        {
        }
    }

    private class ConfigModel
    {
        public string? ServerUrl { get; set; }
    }
}