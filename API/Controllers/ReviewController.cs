using System.Security.Claims;
using Application.Abstarcts.Services;
using Application.Dtos.Review;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ReviewController : ControllerBase
{
    private readonly IReviewService _reviewService;

    public ReviewController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create(CreateReviewRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        await _reviewService.CreateAsync(request, userId);

        return StatusCode(201, new { message = "Review created successfully" });
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateReviewRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        await _reviewService.UpdateAsync(id, request, userId);

        return Ok(new { message = "Review updated successfully" });
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        await _reviewService.DeleteAsync(id, userId);

        return Ok(new { message = "Review deleted successfully" });
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var reviews = await _reviewService.GetAllAsync();

        return Ok(reviews);
    }

    [AllowAnonymous]
    [HttpGet("service/{serviceId}")]
    public async Task<IActionResult> GetByService(int serviceId)
    {
        var reviews = await _reviewService.GetByServiceIdAsync(serviceId);

        return Ok(reviews);
    }
}