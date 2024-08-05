using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using SmartHomeAutomation.Models;
using SmartHomeAutomation.Services.Interfaces;

namespace SmartHomeAutomation.Services.Repositories;

/// <summary>
/// Repository for managing user profiles.
/// </summary>
public class UserProfileRepository : IUserProfileRepository
{
    private readonly ILogger<UserProfileRepository> _logger;
    private readonly Container _container;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserProfileRepository"/> class.
    /// </summary>
    /// <param name="logger">Logger instance.</param>
    /// <param name="cosmosClient">Cosmos client instance.</param>
    public UserProfileRepository(ILogger<UserProfileRepository> logger, CosmosClient cosmosClient)
    {
        _logger = logger;
        _container = cosmosClient.GetContainer("user-profile-db", "UserProfiles");
    }

    /// <inheritdoc/>
    public async Task SaveUserProfileAsync(UserProfile userProfile)
    {
        await _container.UpsertItemAsync(userProfile, new PartitionKey(userProfile.UserId));
        _logger.LogInformation($"User profile saved: {userProfile.UserId}");
    }

    /// <inheritdoc/>
    public async Task<UserProfile> GetUserProfileAsync(string userId)
    {
        try
        {
            var response = await _container.ReadItemAsync<UserProfile>(userId, new PartitionKey(userId));
            return response.Resource;
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            _logger.LogWarning($"User profile not found: {userId}");
            return null;
        }
    }
}