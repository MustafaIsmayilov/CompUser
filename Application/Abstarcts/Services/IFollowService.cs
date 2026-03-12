using Application.Dtos.Follow;

namespace Application.Abstarcts.Services;

public interface IFollowService
{
    Task FollowCompanyAsync(int companyId, string userId);

    Task UnfollowAsync(int followId, string userId);

    Task<List<FollowResponse>> GetMyFollowsAsync(string userId);
}
