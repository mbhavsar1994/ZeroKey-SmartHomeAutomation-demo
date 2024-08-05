using SmartHomeAutomation.Models;

namespace SmartHomeAutomation.Services.Interfaces;

/// <summary>
/// Interface for managing Smart Thermostat data.
/// </summary>
public interface ISmartThermostatService
{
    Task RecordEventAsync(DeviceEvent deviceEvent);
    Task<DeviceEvent> GetLatestEventAsync(string deviceId);
    Task<IEnumerable<DeviceEvent>> GetEventHistoryAsync(string deviceId);
}