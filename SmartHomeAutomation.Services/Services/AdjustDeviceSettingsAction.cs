using Microsoft.Extensions.Logging;
using SmartHomeAutomation.Services.Interfaces;

namespace SmartHomeAutomation.Services.Services;

/// <summary>
/// Action to adjust device settings.
/// </summary>
public class AdjustDeviceSettingsAction : IAutomationAction
{
    private readonly ILogger<AdjustDeviceSettingsAction> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="AdjustDeviceSettingsAction"/> class.
    /// </summary>
    /// <param name="logger">Logger instance.</param>
    public AdjustDeviceSettingsAction(ILogger<AdjustDeviceSettingsAction> logger)
    {
        _logger = logger;
    }

    /// <inheritdoc/>
    public Task ExecuteAsync(string deviceId, float temperature, float humidity)
    {
        _logger.LogInformation($"Adjusting settings for device: {deviceId} - Temperature: {temperature}, Humidity: {humidity}");
        // Implement logic to adjust device settings here
        return Task.CompletedTask;
    }
}