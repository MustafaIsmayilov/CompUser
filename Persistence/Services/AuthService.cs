using Application.Abstarcts.Services;
using Application.Dtos.Auth;
using Domain.Constants;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IRefreshTokenService _refreshTokenService;

    public AuthService(
        UserManager<AppUser> userManager,
        IJwtTokenGenerator jwtTokenGenerator,
        IRefreshTokenService refreshTokenService)
    {
        _userManager = userManager;
        _jwtTokenGenerator = jwtTokenGenerator;
        _refreshTokenService = refreshTokenService;
    }

    public async Task RegisterAsync(RegisterRequest request)
    {
        var user = new AppUser
        {
            Name = request.Name,
            Email = request.Email,
            UserName = request.Email
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
            throw new Exception(string.Join(",", result.Errors.Select(x => x.Description)));

        await _userManager.AddToRoleAsync(user, RoleNames.User);
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)
            throw new UnauthorizedAccessException("Email or password incorrect");

        var passwordValid =
            await _userManager.CheckPasswordAsync(user, request.Password);

        if (!passwordValid)
            throw new UnauthorizedAccessException("Email or password incorrect");

        var accessToken = await _jwtTokenGenerator.GenerateToken(user);

        var refreshToken =
            await _refreshTokenService.CreateRefreshTokenAsync(user);

        return new LoginResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }

    public async Task<LoginResponse> RefreshAsync(string refreshToken)
    {
        return await _refreshTokenService.RefreshAsync(refreshToken);
    }
}