namespace SmartHomeAutomation.Services.Interfaces;

/// <summary>
/// Interface for evaluating automation rules.
/// </summary>
public interface IRuleEvaluator
{
    /// <summary>
    /// Evaluates and executes rules based on device data.
    /// </summary>
    /// <param name="deviceId">Identifier of the device.</param>
    /// <param name="temperature">Temperature value.</param>
    /// <param name="humidity">Humidity value.</param>
    Task EvaluateAndExecuteRulesAsync(string deviceId, float temperature, float humidity);
}