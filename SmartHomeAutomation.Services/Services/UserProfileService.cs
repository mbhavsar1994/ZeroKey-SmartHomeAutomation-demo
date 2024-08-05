using Microsoft.Extensions.Logging;
using SmartHomeAutomation.Models;
using SmartHomeAutomation.Services.Interfaces;

namespace SmartHomeAutomation.Services.Services;

/// <summary>
/// Service for managing user profiles.
/// </summary>
public class UserProfileService : IUserProfileService
{
    private readonly ILogger<UserProfileService> _logger;
    private readonly IUserProfileRepository _userProfileRepository;

    public UserProfileService(ILogger<UserProfileService> logger, IUserProfileRepository userProfileRepository)
    {
        _logger = logger;
        _userProfileRepository = userProfileRepository;
    }

    public async Task SaveUserProfileAsync(UserProfile userProfile)
    {
        await _userProfileRepository.SaveUserProfileAsync(userProfile);
        _logger.LogInformation($"User profile saved: {userProfile.UserId}");
    }

    public async Task<UserProfile> GetUserProfileAsync(string userId)
    {
        return await _userProfileRepository.GetUserProfileAsync(userId);
    }
}