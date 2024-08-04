namespace SmartHomeAutomation.Models;

/// <summary>
/// Represents a device in the smart home system.
/// </summary>
public class Device
{
    /// <summary>
    /// Unique identifier for the device.
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Name of the device.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Type of the device (e.g., Thermostat, Light).
    /// </summary>
    public string Type { get; set; }
}