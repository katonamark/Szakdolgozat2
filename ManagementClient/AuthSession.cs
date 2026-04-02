using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementClient;

public static class AuthSession
{
    public static string Token { get; set; } = "";
    public static string Username { get; set; } = "";
    public static string FullName { get; set; } = "";

    public static void Clear()
    {
        Token = "";
        Username = "";
        FullName = "";
    }
}