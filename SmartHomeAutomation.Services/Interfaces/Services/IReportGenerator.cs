namespace SmartHomeAutomation.Services.Interfaces;

public interface IReportGenerator
{
    Task<string> GenerateReportAsync(string deviceId);
}