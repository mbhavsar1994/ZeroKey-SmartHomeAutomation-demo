using Azure;
using Azure.Storage.Blobs.Models;

namespace SmartHomeAutomation.Services.Interfaces;

/// <summary>
/// Interface for file storage service.
/// </summary>
public interface IFileStorageService
{
    /// <summary>
    /// Uploads a file.
    /// </summary>
    /// <param name="fileName">Name of the file.</param>
    /// <param name="fileStream">File stream.</param>
    Task<string> UploadFileAsync(string fileName, Stream fileStream);

    /// <summary>ßßß
    /// Downloads a file.
    /// </summary>
    /// <param name="fileName">Name of the file.</param>
    /// <returns>File stream.</returns>
    Task<Stream> DownloadFileAsync(string fileName);
}