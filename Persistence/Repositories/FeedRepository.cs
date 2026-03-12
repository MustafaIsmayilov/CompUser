using Application.Abstarcts.Repositories;
using Application.Dtos.Feed;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Persistence.Repositories;

public class FeedRepository : IFeedRepository
{
    private readonly CompUserDbContext _context;

    public FeedRepository(CompUserDbContext context)
    {
        _context = context;
    }

    public async Task<List<FeedResponse>> GetFeedAsync(string userId)
    {
        var posts = await _context.Posts
            .Include(x => x.Service)
            .ThenInclude(x => x.Company)
            .Where(x =>
                _context.Follows
                .Any(f => f.UserId == userId &&
                          f.CompanyId == x.Service.CompanyId))
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new FeedResponse
            {
                PostId = x.Id,
                Content = x.Content,
                CompanyId = x.Service.CompanyId,
                CompanyName = x.Service.Company.Name,
                CreatedAt = x.CreatedAt
            })
            .ToListAsync();

        return posts;
    }
}