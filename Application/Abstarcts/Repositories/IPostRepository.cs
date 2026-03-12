using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Abstarcts.Repositories;

public interface IPostRepository : IRepository<Post>
{
    Task<List<Post>> GetPostsByServiceIdAsync(int serviceId);
}
