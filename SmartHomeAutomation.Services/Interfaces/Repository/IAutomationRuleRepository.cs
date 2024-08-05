using SmartHomeAutomation.Models;

namespace SmartHomeAutomation.Services.Interfaces;

/// <summary>
/// Interface for automation rule repository.
/// </summary>
public interface IAutomationRuleRepository
{
    /// <summary>
    /// Gets rules for a device.
    /// </summary>
    /// <param name="deviceId">Identifier of the device.</param>
    /// <returns>List of automation rules.</returns>
    Task<IEnumerable<AutomationRule>> GetRulesForDeviceAsync(string deviceId);
}