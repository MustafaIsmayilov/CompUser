using System.Security.Claims;
using Application.Abstarcts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class FollowController : ControllerBase
{
    private readonly IFollowService _followService;

    public FollowController(IFollowService followService)
    {
        _followService = followService;
    }

    [Authorize]
    [HttpPost("{companyId}")]
    public async Task<IActionResult> FollowCompany(int companyId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        await _followService.FollowCompanyAsync(companyId, userId);

        return Ok(new { message = "Company followed successfully" });
    }

    [Authorize]
    [HttpDelete("{followId}")]
    public async Task<IActionResult> Unfollow(int followId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        await _followService.UnfollowAsync(followId, userId);

        return Ok(new { message = "Unfollowed successfully" });
    }

    [Authorize]
    [HttpGet("my")]
    public async Task<IActionResult> MyFollows()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var follows = await _followService.GetMyFollowsAsync(userId);

        return Ok(follows);
    }
}