using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstarcts.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Persistence.Repositories;

public class FollowRepository : Repository<Follow>, IFollowRepository
{
    private readonly CompUserDbContext _context;

    public FollowRepository(CompUserDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<bool> ExistsAsync(string userId, int companyId)
    {
        return await _context.Follows
            .AnyAsync(x => x.UserId == userId && x.CompanyId == companyId);
    }

    public async Task<List<Follow>> GetUserFollowsAsync(string userId)
    {
        return await _context.Follows
            .Where(x => x.UserId == userId)
            .Include(x => x.Company)
            .ToListAsync();
    }
}
