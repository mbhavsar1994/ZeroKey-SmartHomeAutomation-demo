using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartHomeAutomation.Models;
using SmartHomeAutomation.Services.Interfaces;

namespace SmartHomeAutomation.API.Controllers;

    [ApiController]
    [Route("api/[controller]")]
    [Authorize] 
    public class SmartThermostatController : ControllerBase
    {
        private readonly ISmartThermostatService _thermostatService;
        private readonly ILogger<SmartThermostatController> _logger;

        public SmartThermostatController(ISmartThermostatService thermostatService, ILogger<SmartThermostatController> logger)
        {
            _thermostatService = thermostatService;
            _logger = logger;
        }

        /// <summary>
        /// Records a new Smart Thermostat event.
        /// </summary>
        /// <param name="deviceEvent">The event data to record.</param>
        [HttpPost("record")]
        public async Task<IActionResult> RecordEvent([FromBody] DeviceEvent deviceEvent)
        {
            if (deviceEvent == null)
            {
                return BadRequest("Device event is null");
            }

            await _thermostatService.RecordEventAsync(deviceEvent);
            _logger.LogInformation($"Recorded event for device {deviceEvent.DeviceId}");
            return Ok();
        }

        /// <summary>
        /// Retrieves the latest Smart Thermostat event for a specific device.
        /// </summary>
        /// <param name="deviceId">The ID of the device.</param>
        /// <returns>The latest event data.</returns>
        [HttpGet("latest/{deviceId}")]
        public async Task<IActionResult> GetLatestEvent(string deviceId)
        {
            var latestEvent = await _thermostatService.GetLatestEventAsync(deviceId);
            if (latestEvent == null)
            {
                return NotFound();
            }

            _logger.LogInformation($"Retrieved latest event for device {deviceId}");
            return Ok(latestEvent);
        }

        /// <summary>
        /// Retrieves the event history for a specific Smart Thermostat device.
        /// </summary>
        /// <param name="deviceId">The ID of the device.</param>
        /// <returns>A list of event data.</returns>
        [HttpGet("history/{deviceId}")]
        public async Task<IActionResult> GetEventHistory(string deviceId)
        {
            var eventHistory = await _thermostatService.GetEventHistoryAsync(deviceId);
            if (eventHistory == null || !eventHistory.Any())
            {
                return NotFound();
            }

            _logger.LogInformation($"Retrieved event history for device {deviceId}");
            return Ok(eventHistory);
        }
    }