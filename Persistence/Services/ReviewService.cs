using Application.Abstarcts.Repositories;
using Application.Abstarcts.Services;
using Application.Dtos.Review;
using Domain.Entities;

namespace Persistence.Services;

public class ReviewService : IReviewService
{
    private readonly IReviewRepository _reviewRepository;

    public ReviewService(IReviewRepository reviewRepository)
    {
        _reviewRepository = reviewRepository;
    }

    public async Task CreateAsync(CreateReviewRequest request, string userId)
    {
        var review = new Review
        {
            Rating = request.Rating,
            Comment = request.Comment,
            ServiceId = request.ServiceId,
            UserId = userId
        };

        await _reviewRepository.AddAsync(review);
        await _reviewRepository.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, UpdateReviewRequest request, string userId)
    {
        var review = await _reviewRepository.GetByIdAsync(id);

        if (review == null)
            throw new KeyNotFoundException("Review not found");

        if (review.UserId != userId)
            throw new UnauthorizedAccessException("You cannot edit this review");

        review.Rating = request.Rating;
        review.Comment = request.Comment;

        _reviewRepository.Update(review);

        await _reviewRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id, string userId)
    {
        var review = await _reviewRepository.GetByIdAsync(id);

        if (review == null)
            throw new KeyNotFoundException("Review not found");

        if (review.UserId != userId)
            throw new UnauthorizedAccessException("You cannot delete this review");

        _reviewRepository.Remove(review);

        await _reviewRepository.SaveChangesAsync();
    }

    public async Task<List<ReviewResponse>> GetAllAsync()
    {
        var reviews = await _reviewRepository.GetAllAsync();

        return reviews.Select(x => new ReviewResponse
        {
            Id = x.Id,
            Rating = x.Rating,
            Comment = x.Comment,
            ServiceId = x.ServiceId
        }).ToList();
    }

    public async Task<List<ReviewResponse>> GetByServiceIdAsync(int serviceId)
    {
        var reviews = await _reviewRepository.GetByServiceIdAsync(serviceId);

        return reviews.Select(x => new ReviewResponse
        {
            Id = x.Id,
            Rating = x.Rating,
            Comment = x.Comment,
            ServiceId = x.ServiceId
        }).ToList();
    }
}