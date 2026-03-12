using System.Security.Claims;
using Application.Abstarcts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FeedController : ControllerBase
{
    private readonly IFeedService _feedService;

    public FeedController(IFeedService feedService)
    {
        _feedService = feedService;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetFeed()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var feed = await _feedService.GetFeedAsync(userId);

        return Ok(feed);
    }
}