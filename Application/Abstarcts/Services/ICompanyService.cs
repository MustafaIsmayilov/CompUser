using Application.Dtos.Company;
using Domain.Entities;

namespace Application.Abstarcts.Services;

public interface ICompanyService
{
    Task CreateAsync(CreateCompanyRequest request, string userId);
    Task UpdateAsync(int id, UpdateCompanyRequest request, string userId);
    Task DeleteAsync(int id, string userId);
    Task<List<CompanyResponse>> GetAllAsync();
    Task<CompanyResponse> GetByIdAsync(int id);
}
