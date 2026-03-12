using Application.Abstarcts.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Persistence.Repositories;

public class RefreshTokenRepository
    : Repository<RefreshToken>, IRefreshTokenRepository
{
    private readonly CompUserDbContext _context;

    public RefreshTokenRepository(CompUserDbContext context)
        : base(context)
    {
        _context = context;
    }

    public async Task<RefreshToken?> GetByTokenAsync(string token)
    {
        return await _context.RefreshTokens
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Token == token);
    }
}