namespace SmartHomeAutomation.Services.Interfaces;

public interface IReportNotificationService
{
    Task NotifyAsync(string deviceId, string reportUrl);
}