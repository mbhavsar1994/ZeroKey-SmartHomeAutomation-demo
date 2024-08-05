using SmartHomeAutomation.Models;

namespace SmartHomeAutomation.Services.Interfaces;

/// <summary>
/// Interface for device repository.
/// </summary>
public interface IDeviceRepository
{
    /// <summary>
    /// Creates a new device.
    /// </summary>
    /// <param name="device">Device to create.</param>
    Task CreateDeviceAsync(Device device);

    /// <summary>
    /// Gets a device by its identifier.
    /// </summary>
    /// <param name="deviceId">Identifier of the device.</param>
    /// <returns>Device object.</returns>
    Task<Device> GetDeviceAsync(string deviceId);
}