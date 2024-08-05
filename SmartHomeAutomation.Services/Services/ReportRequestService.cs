using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SmartHomeAutomation.Services.Interfaces;

namespace SmartHomeAutomation.Services.Services;

/// <summary>
/// Service for sending report requests.
/// </summary>
public class ReportRequestService : IReportRequestService
{
    private readonly IMessageSender _messageSender;
    private readonly ILogger<ReportRequestService> _logger;
    private readonly string reportRequestQueueName;

    public ReportRequestService(IMessageSender messageSender, 
        ILogger<ReportRequestService> logger,
        IConfiguration configuration)
    {
        _messageSender = messageSender;
        _logger = logger;
        reportRequestQueueName = configuration["ReportRequestQueue"];
    }

    /// <inheritdoc/>
    public async Task SendReportRequestAsync(string deviceId)
    {
        string message = deviceId;
        await _messageSender.SendMessageAsync(reportRequestQueueName, message);
        _logger.LogInformation($"Report request sent for device {deviceId}");
    }
}