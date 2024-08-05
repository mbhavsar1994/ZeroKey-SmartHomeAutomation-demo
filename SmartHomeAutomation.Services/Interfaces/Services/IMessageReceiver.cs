namespace SmartHomeAutomation.Services.Interfaces;

/// <summary>
/// Interface for message receiver.
/// </summary>
public interface IMessageReceiver
{
    /// <summary>
    /// Receives messages from a queue or topic.
    /// </summary>
    /// <param name="queueOrTopicName">Queue or topic name.</param>
    Task ReceiveMessagesAsync(string queueOrTopicName);
}