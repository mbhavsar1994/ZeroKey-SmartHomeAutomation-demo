namespace SmartHomeAutomation.Services.Interfaces;

public interface ISignalRNotificationService
{
    Task NotifyClientAsync(string deviceId, string message);
}