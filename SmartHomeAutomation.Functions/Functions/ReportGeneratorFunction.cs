using System;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace SmartHomeAutomation.Functions.Functions;

public class ReportGeneratorFunction
{
    private readonly ILogger<ReportGeneratorFunction> _logger;

    public ReportGeneratorFunction(ILogger<ReportGeneratorFunction> logger)
    {
        _logger = logger;
    }

    [Function(nameof(ReportGeneratorFunction))]
    public void Run([ServiceBusTrigger("myqueue", Connection = "")] ServiceBusReceivedMessage message)
    {
        _logger.LogInformation("Message ID: {id}", message.MessageId);
        _logger.LogInformation("Message Body: {body}", message.Body);
        _logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);
        
    }
}