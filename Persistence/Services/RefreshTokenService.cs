using Application.Abstarcts.Repositories;
using Application.Abstarcts.Services;
using Application.Dtos.Auth;
using Domain.Entities;

public class RefreshTokenService : IRefreshTokenService
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public RefreshTokenService(
        IRefreshTokenRepository refreshTokenRepository,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<string> CreateRefreshTokenAsync(AppUser user)
    {
        var token = Guid.NewGuid().ToString();

        var refreshToken = new RefreshToken
        {
            Token = token,
            UserId = user.Id,
            ExpireDate = DateTime.UtcNow.AddDays(7),
            IsRevoked = false
        };

        await _refreshTokenRepository.AddAsync(refreshToken);
        await _refreshTokenRepository.SaveChangesAsync();

        return token;
    }

    public async Task<LoginResponse> RefreshAsync(string refreshToken)
    {
        var tokenEntity =
            await _refreshTokenRepository.GetByTokenAsync(refreshToken);

        if (tokenEntity == null)
            throw new UnauthorizedAccessException("Invalid refresh token");

        if (tokenEntity.IsRevoked)
            throw new UnauthorizedAccessException("Token revoked");

        if (tokenEntity.ExpireDate < DateTime.UtcNow)
            throw new UnauthorizedAccessException("Token expired");

        var newAccessToken =await _jwtTokenGenerator.GenerateToken(tokenEntity.User);

        return new LoginResponse
        {
            AccessToken = newAccessToken
        };
    }

    public async Task RevokeAsync(string refreshToken)
    {
        var token = await _refreshTokenRepository.GetByTokenAsync(refreshToken);

        if (token == null)
            throw new Exception("Refresh token not found");

        token.IsRevoked = true;

        await _refreshTokenRepository.SaveChangesAsync();
    }
}
