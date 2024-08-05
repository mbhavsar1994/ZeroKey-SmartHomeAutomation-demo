using Azure.Messaging.EventHubs;

namespace SmartHomeAutomation.Services.Interfaces;

/// <summary>
/// Interface for handling Event Hub triggers.
/// </summary>
public interface IEventHubTriggerHandler
{
    /// <summary>
    /// Handles Event Hub events.
    /// </summary>
    /// <param name="events">Array of EventData.</param>
    Task HandleEventsAsync(EventData[] events);
}