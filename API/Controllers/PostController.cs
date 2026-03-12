using System.Security.Claims;
using Application.Abstarcts.Services;
using Application.Dtos.Post;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PostController : ControllerBase
{
    private readonly IPostService _postService;

    public PostController(IPostService postService)
    {
        _postService = postService;
    }

    [Authorize(Roles = "Provider")]
    [HttpPost]
    public async Task<IActionResult> Create(CreatePostRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        await _postService.CreateAsync(request, userId);

        return StatusCode(201, new { message = "Post created successfully" });
    }

    [Authorize(Roles = "Provider")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdatePostRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        await _postService.UpdateAsync(id, request, userId);

        return Ok(new { message = "Post updated successfully" });
    }

    [Authorize(Roles = "Provider")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        await _postService.DeleteAsync(id, userId);

        return Ok(new { message = "Post deleted successfully" });
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var posts = await _postService.GetAllAsync();

        return Ok(posts);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var post = await _postService.GetByIdAsync(id);

        return Ok(post);
    }
}