using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.SignalR.Client;
using SmartHomeAutomation.Services.Interfaces;

namespace SmartHomeAutomation.Services.Services;

public class SignalRNotificationService : ISignalRNotificationService
{
    private readonly string _signalRHubUrl;
    private readonly ILogger<SignalRNotificationService> _logger;

    public SignalRNotificationService(IConfiguration configuration, ILogger<SignalRNotificationService> logger)
    {
        _signalRHubUrl = configuration["SignalRHubUrl"];
        _logger = logger;
    }

    public async Task NotifyClientAsync(string deviceId, string message)
    {
        var connection = new HubConnectionBuilder()
            .WithUrl(_signalRHubUrl)
            .Build();

        await connection.StartAsync();
        await connection.InvokeAsync("SendMessageToClient", deviceId, message);
        await connection.StopAsync();

        _logger.LogInformation($"Notification sent to device {deviceId}: {message}");
    }
}