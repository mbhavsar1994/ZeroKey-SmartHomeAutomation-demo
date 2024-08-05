using System;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using SmartHomeAutomation.Services.Interfaces;

namespace SmartHomeAutomation.Functions.Functions;

public class ReportGeneratorFunction
{
    private readonly IReportGenerator _reportGenerator;
    private readonly IReportNotificationService _reportNotificationService;
    private readonly ILogger<ReportGeneratorFunction> _logger;

    public ReportGeneratorFunction(IReportGenerator reportGenerator, IReportNotificationService reportNotificationService, ILogger<ReportGeneratorFunction> logger)
    {
        _reportGenerator = reportGenerator;
        _reportNotificationService = reportNotificationService;
        _logger = logger;
    }

    [Function(nameof(ReportGeneratorFunction))]
    public async Task Run(
        [ServiceBusTrigger("ReportRequestQueue", Connection = "ServiceBusConnectionString")] ServiceBusReceivedMessage message,
        ServiceBusMessageActions messageActions)
    {
        var deviceId = message.Body.ToString();
        _logger.LogInformation($"Processing report generation for device {deviceId}");

        // Generate report
        var reportUrl = await _reportGenerator.GenerateReportAsync(deviceId);

        // Send notification
        await _reportNotificationService.NotifyAsync(deviceId, reportUrl);

        // Complete message
        await messageActions.CompleteMessageAsync(message);
    }
}