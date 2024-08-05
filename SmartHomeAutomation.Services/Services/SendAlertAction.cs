using Microsoft.Extensions.Logging;
using SmartHomeAutomation.Services.Interfaces;

namespace SmartHomeAutomation.Services.Services;


/// <summary>
/// Action to send an alert.
/// </summary>
public class SendAlertAction : IAutomationAction
{
    private readonly ILogger<SendAlertAction> _logger;
    private readonly IAlertService _alertService;

    /// <summary>
    /// Initializes a new instance of the <see cref="SendAlertAction"/> class.
    /// </summary>
    /// <param name="logger">Logger instance.</param>
    /// <param name="alertService">Alert service instance.</param>
    public SendAlertAction(ILogger<SendAlertAction> logger, IAlertService alertService)
    {
        _logger = logger;
        _alertService = alertService;
    }

    /// <inheritdoc/>
    public async Task ExecuteAsync(string deviceId, float temperature, float humidity)
    {
        _logger.LogInformation($"Sending alert for device: {deviceId} - Temperature: {temperature}, Humidity: {humidity}");
        await _alertService.SendAlertAsync(deviceId, temperature, humidity);
    }
}