namespace SmartHomeAutomation.Services.Interfaces;

/// <summary>
/// Interface for automation actions.
/// </summary>
public interface IAutomationAction
{
    /// <summary>
    /// Executes an action based on device data.
    /// </summary>
    /// <param name="deviceId">Identifier of the device.</param>
    /// <param name="temperature">Temperature value.</param>
    /// <param name="humidity">Humidity value.</param>
    Task ExecuteAsync(string deviceId, float temperature, float humidity);
}