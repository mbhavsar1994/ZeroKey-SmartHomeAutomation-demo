using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartHomeAutomation.Models;
using SmartHomeAutomation.Services.Interfaces;

namespace SmartHomeAutomation.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] 
public class SmartThermostatReportController : ControllerBase
{
    private readonly IReportRequestService _reportRequestService;
    private readonly ILogger<SmartThermostatReportController> _logger;

    public SmartThermostatReportController(IReportRequestService reportRequestService, 
        ILogger<SmartThermostatReportController> logger)
    {
        _reportRequestService = reportRequestService;
        _logger = logger;
    }

    /// <summary>
    /// Requests the generation of an energy consumption report.
    /// </summary>
    [HttpPost("request")]
    public async Task<IActionResult> RequestReportGeneration([FromBody] ReportRequest reportRequest)
    {
        if (reportRequest == null || string.IsNullOrEmpty(reportRequest.DeviceId))
        {
            return BadRequest("Invalid report request.");
        }

        await _reportRequestService.SendReportRequestAsync(reportRequest.DeviceId);
        _logger.LogInformation($"Report generation requested for device {reportRequest.DeviceId}");

        return Accepted();
    }
}