using SmartHomeAutomation.Models;

namespace SmartHomeAutomation.Services.Interfaces;

/// <summary>
/// Interface for user profile repository.
/// </summary>
public interface IUserProfileRepository
{
    /// <summary>
    /// Creates or updates a user profile.
    /// </summary>
    /// <param name="userProfile">User profile to create or update.</param>
    Task SaveUserProfileAsync(UserProfile userProfile);

    /// <summary>
    /// Gets a user profile by user ID.
    /// </summary>
    /// <param name="userId">User ID.</param>
    /// <returns>User profile object.</returns>
    Task<UserProfile> GetUserProfileAsync(string userId);
}