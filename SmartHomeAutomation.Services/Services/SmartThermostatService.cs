using Microsoft.Extensions.Logging;
using SmartHomeAutomation.Models;
using SmartHomeAutomation.Services.Interfaces;

namespace SmartHomeAutomation.Services.Services;

/// <summary>
/// Service for managing Smart Thermostat data.
/// </summary>
public class SmartThermostatService : ISmartThermostatService
{
    private readonly ILogger<SmartThermostatService> _logger;
    private readonly IDeviceEventRepository _deviceEventRepository;

    public SmartThermostatService(ILogger<SmartThermostatService> logger, IDeviceEventRepository deviceEventRepository)
    {
        _logger = logger;
        _deviceEventRepository = deviceEventRepository;
    }

    public async Task RecordEventAsync(DeviceEvent deviceEvent)
    {
        await _deviceEventRepository.StoreEventDataAsync(deviceEvent);
        _logger.LogInformation($"Recorded event for device {deviceEvent.DeviceId} at {deviceEvent.Timestamp}");
    }

    public async Task<DeviceEvent> GetLatestEventAsync(string deviceId)
    {
        var events = await _deviceEventRepository.GetEventsByDeviceIdAsync(deviceId);
        return events.OrderByDescending(e => e.Timestamp).FirstOrDefault();
    }

    public async Task<IEnumerable<DeviceEvent>> GetEventHistoryAsync(string deviceId)
    {
        return await _deviceEventRepository.GetEventsByDeviceIdAsync(deviceId);
    }
}