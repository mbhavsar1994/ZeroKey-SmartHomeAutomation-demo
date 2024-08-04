using SmartHomeAutomation.Models;

namespace SmartHomeAutomation.Services.Interfaces;

/// <summary>
/// Interface for device event repository.
/// </summary>
public interface IDeviceEventRepository
{
    /// <summary>
    /// Stores event data.
    /// </summary>
    /// <param name="deviceEvent">Event data to store.</param>
    Task StoreEventDataAsync(DeviceEvent deviceEvent);
}