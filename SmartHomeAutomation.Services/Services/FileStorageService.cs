using Azure.Storage.Blobs;
using Microsoft.Extensions.Logging;
using SmartHomeAutomation.Services.Interfaces;

namespace SmartHomeAutomation.Services.Services;

/// <summary>
/// Service for managing file storage.
/// </summary>
public class FileStorageService : IFileStorageService
{
    private readonly ILogger<FileStorageService> _logger;
    private readonly BlobContainerClient _containerClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="FileStorageService"/> class.
    /// </summary>
    /// <param name="logger">Logger instance.</param>
    /// <param name="blobServiceClient">Blob service client instance.</param>
    public FileStorageService(ILogger<FileStorageService> logger, BlobServiceClient blobServiceClient)
    {
        _logger = logger;
        _containerClient = blobServiceClient.GetBlobContainerClient("device-logs");
    }

    /// <inheritdoc/>
    public async Task UploadFileAsync(string fileName, Stream fileStream)
    {
        var blobClient = _containerClient.GetBlobClient(fileName);
        await blobClient.UploadAsync(fileStream);
        _logger.LogInformation($"Uploaded file: {fileName}");
    }

    /// <inheritdoc/>
    public async Task<Stream> DownloadFileAsync(string fileName)
    {
        var blobClient = _containerClient.GetBlobClient(fileName);
        var response = await blobClient.DownloadAsync();
        _logger.LogInformation($"Downloaded file: {fileName}");
        return response.Value.Content;
    }
}