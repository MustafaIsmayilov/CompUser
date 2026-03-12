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

public class PostRepository
    : Repository<Post>, IPostRepository
{
    private readonly CompUserDbContext _context;

    public PostRepository(CompUserDbContext context)
        : base(context)
    {
        _context = context;
    }

    public async Task<List<Post>> GetPostsByServiceIdAsync(int serviceId)
    {
        return await _context.Posts
            .Where(x => x.ServiceId == serviceId)
            .ToListAsync();
    }
}