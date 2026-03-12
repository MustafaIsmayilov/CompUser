using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Abstarcts.Repositories;

public interface IFollowRepository : IRepository<Follow>
{
    Task<bool> ExistsAsync(string userId, int companyId);
    Task<List<Follow>> GetUserFollowsAsync(string userId);
}
