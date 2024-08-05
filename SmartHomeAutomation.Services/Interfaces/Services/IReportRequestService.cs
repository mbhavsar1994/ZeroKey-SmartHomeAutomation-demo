namespace SmartHomeAutomation.Services.Interfaces;

/// <summary>
/// Interface for sending report requests.
/// </summary>
public interface IReportRequestService
{
    Task SendReportRequestAsync(string deviceId);
}
