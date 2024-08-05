namespace SmartHomeAutomation.Models;

/// <summary>
/// Represents a user profile in the smart home system.
/// </summary>
public class UserProfile
{
    /// <summary>
    /// Unique identifier for the user.
    /// </summary>
    public string UserId { get; set; }

    /// <summary>
    /// User's name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// User's email address.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// User's preferences and configurations.
    /// </summary>
    public string Preferences { get; set; }
}