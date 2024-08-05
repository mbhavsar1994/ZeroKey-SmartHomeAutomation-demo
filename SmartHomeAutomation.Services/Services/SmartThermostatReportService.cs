using Azure.Storage.Blobs;
using Microsoft.Extensions.Logging;
using SmartHomeAutomation.Services.Interfaces;

namespace SmartHomeAutomation.Services.Services;

public class SmartThermostatReportService : ISmartThermostatReportService
{
    private readonly BlobServiceClient _blobServiceClient;
    private readonly ILogger<SmartThermostatReportService> _logger;

    public SmartThermostatReportService(BlobServiceClient blobServiceClient, ILogger<SmartThermostatReportService> logger)
    {
        _blobServiceClient = blobServiceClient;
        _logger = logger;
    }

    public async Task<string> UploadReportAsync(string fileName, Stream fileStream)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient("energy-consumption-reports");
        await containerClient.CreateIfNotExistsAsync(Azure.Storage.Blobs.Models.PublicAccessType.Blob);
        var blobClient = containerClient.GetBlobClient(fileName);
        await blobClient.UploadAsync(fileStream, overwrite: true);

        _logger.LogInformation($"Report uploaded to {blobClient.Uri}");
        return blobClient.Uri.ToString();
    }
}