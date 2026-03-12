using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos.Feed;

namespace Application.Abstarcts.Repositories;

public interface IFeedRepository
{
    Task<List<FeedResponse>> GetFeedAsync(string userId);
}
