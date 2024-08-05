using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
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
    /// <param name="configuration">Configuration instance.</param>
    public FileStorageService(ILogger<FileStorageService> logger, 
        BlobServiceClient blobServiceClient,
        IConfiguration configuration)
    {
        _logger = logger;
        var blobContainerName = configuration["ReportBlobContainer"];
        _containerClient = blobServiceClient.GetBlobContainerClient(blobContainerName);
    }

    /// <inheritdoc/>
    public async Task<string> UploadFileAsync(string fileName, Stream fileStream)
    {
        var blobClient = _containerClient.GetBlobClient(fileName);
        await blobClient.UploadAsync(fileStream, overwrite:true);
        _logger.LogInformation($"Uploaded file: {fileName}");
        return blobClient.Uri.ToString();
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