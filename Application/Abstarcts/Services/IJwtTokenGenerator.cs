using Domain.Entities;

namespace Application.Abstarcts.Services;

public interface IJwtTokenGenerator
{
    Task<string> GenerateToken(AppUser user);
}
