using Microsoft.AspNetCore.Mvc;
using SmartHomeAutomation.Models;
using SmartHomeAutomation.Services.Interfaces;

namespace SmartHomeAutomation.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserProfileController : ControllerBase
{
    private readonly IUserProfileService _userProfileService;
    private readonly ILogger<UserProfileController> _logger;

    public UserProfileController(IUserProfileService userProfileService, ILogger<UserProfileController> logger)
    {
        _userProfileService = userProfileService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> SaveUserProfile([FromBody] UserProfile userProfile)
    {
        await _userProfileService.SaveUserProfileAsync(userProfile);
        _logger.LogInformation($"User profile saved: {userProfile.UserId}");
        return Ok(userProfile);
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserProfile(string userId)
    {
        var userProfile = await _userProfileService.GetUserProfileAsync(userId);
        if (userProfile == null)
        {
            return NotFound();
        }

        _logger.LogInformation($"User profile retrieved: {userId}");
        return Ok(userProfile);
    }
}