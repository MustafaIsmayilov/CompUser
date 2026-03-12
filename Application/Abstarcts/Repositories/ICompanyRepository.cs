using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Abstarcts.Repositories;

public interface ICompanyRepository : IRepository<Company>
{
    Task<List<Company>> GetCompaniesWithServicesAsync();
}
