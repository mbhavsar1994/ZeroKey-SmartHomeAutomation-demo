using Microsoft.Extensions.Logging;
using SmartHomeAutomation.Services.Interfaces;

namespace SmartHomeAutomation.Services.Services;

/// <summary>
/// Service for sending alerts.
/// </summary>
public class AlertService : IAlertService
{
    private readonly ILogger<AlertService> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="AlertService"/> class.
    /// </summary>
    /// <param name="logger">Logger instance.</param>
    public AlertService(ILogger<AlertService> logger)
    {
        _logger = logger;
    }

    /// <inheritdoc/>
    public Task SendAlertAsync(string deviceId, float temperature, float humidity)
    {
        _logger.LogInformation($"Alert! Device: {deviceId}, Temperature: {temperature}, Humidity: {humidity}");
        // Implement logic to send alert (e.g., email, SMS, push notification)
        return Task.CompletedTask;
    }
}