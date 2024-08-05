using System.Text;
using Microsoft.Extensions.Logging;
using SmartHomeAutomation.Services.Interfaces;

namespace SmartHomeAutomation.Services.Services;

public class ReportGenerator : IReportGenerator
{
    private readonly IFileStorageService _fileStorageService;
    private readonly ILogger<ReportGenerator> _logger;

    public ReportGenerator(IFileStorageService fileStorageService, ILogger<ReportGenerator> logger)
    {
        _fileStorageService = fileStorageService;
        _logger = logger;
    }

    public async Task<string> GenerateReportAsync(string deviceId)
    {
        // Generate report (placeholder implementation)
        var reportContent = $"Energy consumption report for device {deviceId} at {DateTime.UtcNow}";
        var reportStream = new MemoryStream(Encoding.UTF8.GetBytes(reportContent));

        // Upload report
        var fileName = $"{deviceId}-report-{DateTime.UtcNow:yyyyMMddHHmmss}.txt";
        var url = await _fileStorageService.UploadFileAsync(fileName, reportStream);

        _logger.LogInformation($"Report generated for device {deviceId}: {fileName}");
        return url;
    }
}