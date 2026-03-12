using Application.Dtos.Service;
using Domain.Entities;

namespace Application.Abstarcts.Services;

using Application.Dtos.Service;

public interface IServiceService
{
    Task CreateAsync(CreateServiceRequest request, string userId);
    Task UpdateAsync(int id, UpdateServiceRequest request, string userId);
    Task DeleteAsync(int id, string userId);
    Task<List<ServiceResponse>> GetAllAsync();
    Task<ServiceResponse> GetByIdAsync(int id);
}
