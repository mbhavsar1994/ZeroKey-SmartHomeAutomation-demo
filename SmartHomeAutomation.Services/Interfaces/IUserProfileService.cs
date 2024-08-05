using SmartHomeAutomation.Models;

namespace SmartHomeAutomation.Services.Interfaces;

/// <summary>
/// Interface for user profile service.
/// </summary>
public interface IUserProfileService
{
    /// <summary>
    /// Saves a user profile.
    /// </summary>
    /// <param name="userProfile">User profile to save.</param>
    Task SaveUserProfileAsync(UserProfile userProfile);

    /// <summary>
    /// Gets a user profile by user ID.
    /// </summary>
    /// <param name="userId">User ID.</param>
    /// <returns>User profile object.</returns>
    Task<UserProfile> GetUserProfileAsync(string userId);
}