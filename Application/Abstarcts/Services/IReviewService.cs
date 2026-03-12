using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstarcts.Repositories;
using Application.Dtos.Review;
using Domain.Entities;

namespace Application.Abstarcts.Services;


public interface IReviewService
{
    Task CreateAsync(CreateReviewRequest request, string userId);
    Task UpdateAsync(int id, UpdateReviewRequest request, string userId);
    Task DeleteAsync(int id, string userId);
    Task<List<ReviewResponse>> GetAllAsync();
    Task<List<ReviewResponse>> GetByServiceIdAsync(int serviceId);
}
