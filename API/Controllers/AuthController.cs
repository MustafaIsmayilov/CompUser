using Application.Abstarcts.Services;
using Application.Dtos.Auth;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IRefreshTokenService _refreshTokenService;

    public AuthController(
        IAuthService authService,
        IRefreshTokenService refreshTokenService)
    {
        _authService = authService;
        _refreshTokenService = refreshTokenService;
    }

    // REGISTER
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        await _authService.RegisterAsync(request);

        return Ok("User registered successfully");
    }

    // LOGIN
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var result = await _authService.LoginAsync(request);

        return Ok(result);
    }

    // REFRESH TOKEN
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(string refreshToken)
    {
        var result = await _authService.RefreshAsync(refreshToken);

        return Ok(result);
    }

    // LOGOUT
    [HttpPost("logout")]
    public async Task<IActionResult> Logout(string refreshToken)
    {
        await _refreshTokenService.RevokeAsync(refreshToken);

        return Ok("Logged out successfully");
    }
}