using Application.Abstarcts.Repositories;
using Application.Abstarcts.Services;
using Application.Dtos.Feed;

namespace Persistence.Services;

public class FeedService : IFeedService
{
    private readonly IFeedRepository _feedRepository;

    public FeedService(IFeedRepository feedRepository)
    {
        _feedRepository = feedRepository;
    }

    public async Task<List<FeedResponse>> GetFeedAsync(string userId)
    {
        return await _feedRepository.GetFeedAsync(userId);
    }
}