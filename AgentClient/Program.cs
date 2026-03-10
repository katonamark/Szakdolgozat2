using Microsoft.AspNetCore.SignalR.Client;

var connection = new HubConnectionBuilder()
    .WithUrl("https://localhost:7294/agenthub") // ide a te szerver portod
    .WithAutomaticReconnect()
    .Build();

connection.On<string>("ReceiveMessage", (message) =>
{
    Console.WriteLine($"Üzenet a szervertől: {message}");
});

try
{
    await connection.StartAsync();
    Console.WriteLine("Kapcsolódva a szerverhez.");

    await connection.InvokeAsync("RegisterAgent", Environment.MachineName);

    Console.WriteLine("Agent regisztrálva.");
    Console.ReadLine();
}
catch (Exception ex)
{
    Console.WriteLine("Hiba: " + ex.Message);
}

