using ManagementServer;
using ManagementServer.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<AgentRegistry>();
builder.Services.AddSingleton<AuthService>();
builder.Services.AddControllers();

builder.Services.AddSignalR(options =>
{
    options.MaximumReceiveMessageSize = 50 * 1024 * 1024; //50MB
    options.EnableDetailedErrors = true;
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("LocalDevOnly", policy =>
    {
        policy
            .SetIsOriginAllowed(origin =>
            {
                if (string.IsNullOrWhiteSpace(origin))
                {
                    return false;
                }

                return origin.StartsWith("https://localhost", StringComparison.OrdinalIgnoreCase)
                    || origin.StartsWith("http://localhost", StringComparison.OrdinalIgnoreCase)
                    || origin.StartsWith("https://127.0.0.1", StringComparison.OrdinalIgnoreCase)
                    || origin.StartsWith("http://127.0.0.1", StringComparison.OrdinalIgnoreCase)
                    || origin.StartsWith("https://192.168.", StringComparison.OrdinalIgnoreCase)
                    || origin.StartsWith("http://192.168.", StringComparison.OrdinalIgnoreCase);
            })
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

builder.WebHost.UseUrls("http://0.0.0.0:5000");
var app = builder.Build();

app.UseHttpsRedirection();
app.UseCors("LocalDevOnly");

app.MapControllers();
app.MapHub<AgentHub>("/agenthub");

app.Run();