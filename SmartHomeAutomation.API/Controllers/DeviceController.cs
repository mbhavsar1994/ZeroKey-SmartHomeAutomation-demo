using Microsoft.AspNetCore.Mvc;
using SmartHomeAutomation.Models;
using SmartHomeAutomation.Services.Interfaces;

namespace SmartHomeAutomation.API.Controllers;

/// <summary>
/// API controller for managing devices.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class DeviceController : ControllerBase
{
    private readonly IDeviceRepository _deviceRepository;
    private readonly ILogger<DeviceController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeviceController"/> class.
    /// </summary>
    /// <param name="deviceRepository">Device repository instance.</param>
    /// <param name="logger">Logger instance.</param>
    public DeviceController(IDeviceRepository deviceRepository, ILogger<DeviceController> logger)
    {
        _deviceRepository = deviceRepository;
        _logger = logger;
    }

    /// <summary>
    /// Creates a new device.
    /// </summary>
    /// <param name="device">Device to create.</param>
    /// <returns>Action result.</returns>
    [HttpPost]
    public async Task<IActionResult> CreateDevice([FromBody] Device device)
    {
        await _deviceRepository.CreateDeviceAsync(device);
        _logger.LogInformation($"Device created: {device.Id}");
        return Ok(device);
    }

    /// <summary>
    /// Gets a device by its identifier.
    /// </summary>
    /// <param name="id">Identifier of the device.</param>
    /// <returns>Action result.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetDevice(string id)
    {
        var device = await _deviceRepository.GetDeviceAsync(id);
        if (device == null)
        {
            return NotFound();
        }

        _logger.LogInformation($"Device retrieved: {id}");
        return Ok(device);
    }
}