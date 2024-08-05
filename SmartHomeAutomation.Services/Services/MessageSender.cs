using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Logging;
using SmartHomeAutomation.Services.Interfaces;

namespace SmartHomeAutomation.Services.Services;

/// <summary>
/// Service for sending messages to Azure Service Bus.
/// </summary>
public class MessageSender : IMessageSender
{
    private readonly ILogger<MessageSender> _logger;
    private readonly ServiceBusClient _serviceBusClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="MessageSender"/> class.
    /// </summary>
    /// <param name="logger">Logger instance.</param>
    /// <param name="serviceBusClient">Service Bus client instance.</param>
    public MessageSender(ILogger<MessageSender> logger, ServiceBusClient serviceBusClient)
    {
        _logger = logger;
        _serviceBusClient = serviceBusClient;
    }

    /// <inheritdoc/>
    public async Task SendMessageAsync(string queueOrTopicName, string message)
    {
        ServiceBusSender sender = _serviceBusClient.CreateSender(queueOrTopicName);
        ServiceBusMessage busMessage = new ServiceBusMessage(message);
        await sender.SendMessageAsync(busMessage);
        _logger.LogInformation($"Message sent to {queueOrTopicName}: {message}");
    }
}