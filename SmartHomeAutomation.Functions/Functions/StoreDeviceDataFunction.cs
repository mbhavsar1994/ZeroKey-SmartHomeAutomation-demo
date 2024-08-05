using Azure.Messaging.EventHubs;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using SmartHomeAutomation.Services.Interfaces;

namespace SmartHomeAutomation.Functions.Functions;

/// <summary>
/// Azure Function to process and store device data from Event Hub.
/// </summary>
public class StoreDeviceDataFunction
{
    private readonly ILogger<StoreDeviceDataFunction> _logger;
    private readonly IEventHubTriggerHandler _eventHubTriggerHandler;

    /// <summary>
    /// Initializes a new instance of the <see cref="StoreDeviceDataFunction"/> class.
    /// </summary>
    /// <param name="loggerFactory">Logger factory instance.</param>
    /// <param name="eventHubTriggerHandler">Event Hub trigger handler instance.</param>
    public StoreDeviceDataFunction(ILoggerFactory loggerFactory, IEventHubTriggerHandler eventHubTriggerHandler)
    {
        _logger = loggerFactory.CreateLogger<StoreDeviceDataFunction>();
        _eventHubTriggerHandler = eventHubTriggerHandler;
    }

    /// <summary>
    /// Function entry point for processing Event Hub messages.
    /// </summary>
    /// <param name="events">Array of EventData from Event Hub.</param>
    [Function("StoreDeviceData")]
    public async Task Run(
        [EventHubTrigger("%eventHubName%", Connection = "eventHubConnectionString")] EventData[] events)
    {
        _logger.LogInformation("StoreDeviceData function triggered");

        try
        {
            await _eventHubTriggerHandler.HandleEventsAsync(events);
            _logger.LogInformation("StoreDeviceData function completed successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while processing events: {ex.Message}", ex);
            throw;
        }
    }
}