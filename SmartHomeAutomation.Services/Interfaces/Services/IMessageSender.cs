namespace SmartHomeAutomation.Services.Interfaces;

/// <summary>
/// Interface for message sender.
/// </summary>
public interface IMessageSender
{
    /// <summary>
    /// Sends a message to a queue or topic.
    /// </summary>
    /// <param name="queueOrTopicName">Queue or topic name.</param>
    /// <param name="message">Message to send.</param>
    Task SendMessageAsync(string queueOrTopicName, string message);
}