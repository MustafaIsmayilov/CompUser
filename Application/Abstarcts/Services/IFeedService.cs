using Application.Dtos.Feed;

namespace Application.Abstarcts.Services;

public interface IFeedService
{
    Task<List<FeedResponse>> GetFeedAsync(string userId);
}