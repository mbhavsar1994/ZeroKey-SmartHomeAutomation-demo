using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Logging;
using SmartHomeAutomation.Services.Interfaces;

namespace SmartHomeAutomation.Services.Services;

/// <summary>
/// Service for receiving messages from Azure Service Bus.
/// </summary>
public class MessageReceiver : IMessageReceiver
{
    private readonly ILogger<MessageReceiver> _logger;
    private readonly ServiceBusClient _serviceBusClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="MessageReceiver"/> class.
    /// </summary>
    /// <param name="logger">Logger instance.</param>
    /// <param name="serviceBusClient">Service Bus client instance.</param>
    public MessageReceiver(ILogger<MessageReceiver> logger, 
        ServiceBusClient serviceBusClient)
    {
        _logger = logger;
        _serviceBusClient = serviceBusClient;
    }

    /// <inheritdoc/>
    public async Task ReceiveMessagesAsync(string queueOrTopicName)
    {
        ServiceBusProcessor processor = _serviceBusClient.CreateProcessor(queueOrTopicName);

        processor.ProcessMessageAsync += async args =>
        {
            string body = args.Message.Body.ToString();
            _logger.LogInformation($"Received message: {body}");
            await args.CompleteMessageAsync(args.Message);
        };

        processor.ProcessErrorAsync += async args =>
        {
            _logger.LogError(args.Exception, $"Message handler encountered an exception: {args.Exception.Message}");
            await Task.CompletedTask;
        };

        await processor.StartProcessingAsync();
    }
}