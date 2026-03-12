using Application.Abstarcts.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Persistence.Repositories;

public class CompanyRepository
    : Repository<Company>, ICompanyRepository
{
    private readonly CompUserDbContext _context;

    public CompanyRepository(CompUserDbContext context)
        : base(context)
    {
        _context = context;
    }

    public async Task<List<Company>> GetCompaniesWithServicesAsync()
    {
        return await _context.Companies
            .Include(x => x.Services)
            .ToListAsync();
    }
}
