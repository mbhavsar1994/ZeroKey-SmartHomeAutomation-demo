using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using SmartHomeAutomation.Models;
using SmartHomeAutomation.Services.Interfaces;

namespace SmartHomeAutomation.Services.Repositories;

/// <summary>
/// Repository for managing device events.
/// </summary>
public class DeviceEventRepository : IDeviceEventRepository
{
    private readonly ILogger<DeviceEventRepository> _logger;
    private readonly Container _container;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeviceEventRepository"/> class.
    /// </summary>
    /// <param name="logger">Logger instance.</param>
    /// <param name="cosmosClient">Cosmos client instance.</param>
    public DeviceEventRepository(ILogger<DeviceEventRepository> logger, CosmosClient cosmosClient)
    {
        _logger = logger;
        _container = cosmosClient.GetContainer("device-data-document-db", "DeviceEvents");
    }

    /// <inheritdoc/>
    public async Task StoreEventDataAsync(DeviceEvent deviceEvent)
    {
        await _container.CreateItemAsync(deviceEvent, new PartitionKey(deviceEvent.DeviceId));
        _logger.LogInformation($"Stored event data for device: {deviceEvent.DeviceId}");
    }
}