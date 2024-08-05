namespace SmartHomeAutomation.Services.Interfaces;

public interface ISmartThermostatReportService
{
    Task<string> UploadReportAsync(string fileName, Stream fileStream);
}