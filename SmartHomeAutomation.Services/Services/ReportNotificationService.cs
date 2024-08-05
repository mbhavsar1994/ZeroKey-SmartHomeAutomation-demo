using Microsoft.Extensions.Logging;
using SmartHomeAutomation.Services.Interfaces;

namespace SmartHomeAutomation.Services.Services;

public class ReportNotificationService : IReportNotificationService
{
    private readonly ISignalRNotificationService _signalRNotificationService;
    private readonly ILogger<ReportNotificationService> _logger;

    public ReportNotificationService(ISignalRNotificationService signalRNotificationService, ILogger<ReportNotificationService> logger)
    {
        _signalRNotificationService = signalRNotificationService;
        _logger = logger;
    }

    public async Task NotifyAsync(string deviceId, string reportUrl)
    {
        string message = $"Your energy consumption report is ready. Download it here: {reportUrl}";
        await _signalRNotificationService.NotifyClientAsync(deviceId, message);
        _logger.LogInformation($"Notification sent for device {deviceId} with report URL {reportUrl}");
    }
}