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

public class ServiceRepository
    : Repository<Service>, IServiceRepository
{
    private readonly CompUserDbContext _context;

    public ServiceRepository(CompUserDbContext context)
        : base(context)
    {
        _context = context;
    }

    public async Task<List<Service>> GetServicesByCompanyIdAsync(int companyId)
    {
        return await _context.Services
            .Where(x => x.CompanyId == companyId)
            .ToListAsync();
    }
}
