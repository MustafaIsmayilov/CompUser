using Application.Abstarcts.Repositories;
using Application.Abstarcts.Services;
using Application.Dtos.Follow;
using Domain.Entities;

public class FollowService : IFollowService
{
    private readonly IFollowRepository _followRepository;

    public FollowService(IFollowRepository followRepository)
    {
        _followRepository = followRepository;
    }

    public async Task FollowCompanyAsync(int companyId, string userId)
    {
        var exists = await _followRepository.ExistsAsync(userId, companyId);

        if (exists)
            throw new Exception("Already following this company");

        var follow = new Follow
        {
            UserId = userId,
            CompanyId = companyId
        };

        await _followRepository.AddAsync(follow);

        await _followRepository.SaveChangesAsync();
    }

    public async Task UnfollowAsync(int followId, string userId)
    {
        var follow = await _followRepository.GetByIdAsync(followId);

        if (follow == null)
            throw new KeyNotFoundException("Follow not found");

        if (follow.UserId != userId)
            throw new UnauthorizedAccessException();

        _followRepository.Remove(follow);

        await _followRepository.SaveChangesAsync();
    }

    public async Task<List<FollowResponse>> GetMyFollowsAsync(string userId)
    {
        var follows = await _followRepository.GetUserFollowsAsync(userId);

        return follows.Select(x => new FollowResponse
        {
            Id = x.Id,
            CompanyId = x.CompanyId,
            CompanyName = x.Company.Name
        }).ToList();
    }
}