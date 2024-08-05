using Microsoft.Azure.Cosmos;
using SmartHomeAutomation.Models;
using Microsoft.Extensions.Logging;
using SmartHomeAutomation.Services.Interfaces;

namespace SmartHomeAutomation.Services.Repositories;

/// <summary>
/// Repository for managing devices.
/// </summary>
public class DeviceRepository : IDeviceRepository
{
    private readonly ILogger<DeviceRepository> _logger;
    private readonly Container _container;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeviceRepository"/> class.
    /// </summary>
    /// <param name="logger">Logger instance.</param>
    /// <param name="cosmosClient">Cosmos client instance.</param>
    public DeviceRepository(ILogger<DeviceRepository> logger, CosmosClient cosmosClient)
    {
        _logger = logger;
        _container = cosmosClient.GetContainer("device-data-document-db", "Devices");
    }

    /// <inheritdoc/>
    public async Task CreateDeviceAsync(Device device)
    {
        await _container.CreateItemAsync(device, new PartitionKey(device.Id));
        _logger.LogInformation($"Device created: {device.Id}");
    }

    /// <inheritdoc/>
    public async Task<Device> GetDeviceAsync(string deviceId)
    {
        try
        {
            var response = await _container.ReadItemAsync<Device>(deviceId, new PartitionKey(deviceId));
            return response.Resource;
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            _logger.LogWarning($"Device not found: {deviceId}");
            return null;
        }
    }
}