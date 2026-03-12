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

public class ReviewRepository
    : Repository<Review>, IReviewRepository
{
    private readonly CompUserDbContext _context;

    public ReviewRepository(CompUserDbContext context)
        : base(context)
    {
        _context = context;
    }

    public async Task<List<Review>> GetByServiceIdAsync(int serviceId)
    {
        return await _context.Reviews
            .Where(x => x.ServiceId == serviceId)
            .ToListAsync();
    }
}
