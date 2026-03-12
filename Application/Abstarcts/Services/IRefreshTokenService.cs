using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos.Auth;
using Domain.Entities;

namespace Application.Abstarcts.Services;

public interface IRefreshTokenService
{
    Task<string> CreateRefreshTokenAsync(AppUser user);
    Task<LoginResponse> RefreshAsync(string refreshToken);
    Task RevokeAsync(string refreshToken);
}
