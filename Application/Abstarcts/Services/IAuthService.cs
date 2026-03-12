namespace Application.Abstarcts.Services;

using Application.Dtos.Auth;

public interface IAuthService
{
    Task RegisterAsync(RegisterRequest request);

    Task<LoginResponse> LoginAsync(LoginRequest request);

    Task<LoginResponse> RefreshAsync(string refreshToken);
}
