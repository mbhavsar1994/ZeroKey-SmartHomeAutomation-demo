namespace SmartHomeAutomation.Services.Interfaces;

/// <summary>
/// Interface for alert service.
/// </summary>
public interface IAlertService
{
    /// <summary>
    /// Sends an alert based on device data.
    /// </summary>
    /// <param name="deviceId">Identifier of the device.</param>
    /// <param name="temperature">Temperature value.</param>
    /// <param name="humidity">Humidity value.</param>
    Task SendAlertAsync(string deviceId, float temperature, float humidity);
}