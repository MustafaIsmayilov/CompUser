using Application.Abstarcts.Repositories;
using Application.Abstarcts.Services;
using Application.Dtos.Company;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Persistence.Services;

public class CompanyService : ICompanyService
{
    private readonly ICompanyRepository _companyRepository;
    private readonly UserManager<AppUser> _userManager;

    public CompanyService(
        ICompanyRepository companyRepository,
        UserManager<AppUser> userManager)
    {
        _companyRepository = companyRepository;
        _userManager = userManager;
    }

    // CREATE COMPANY
    public async Task CreateAsync(CreateCompanyRequest request, string userId)
    {
        var company = new Company
        {
            Name = request.Name,
            OwnerId = userId
        };

        await _companyRepository.AddAsync(company);
        await _companyRepository.SaveChangesAsync();

        var user = await _userManager.FindByIdAsync(userId);

        if (user != null && !await _userManager.IsInRoleAsync(user, "Provider"))
        {
            await _userManager.AddToRoleAsync(user, "Provider");
        }
    }

    // UPDATE COMPANY
    public async Task UpdateAsync(int id, UpdateCompanyRequest request, string userId)
    {
        var company = await _companyRepository.GetByIdAsync(id);

        if (company == null)
            throw new KeyNotFoundException("Company not found");

        if (company.OwnerId != userId)
            throw new UnauthorizedAccessException("You cannot update this company");

        company.Name = request.Name;

        _companyRepository.Update(company);

        await _companyRepository.SaveChangesAsync();
    }

    // DELETE COMPANY
    public async Task DeleteAsync(int id, string userId)
    {
        var company = await _companyRepository.GetByIdAsync(id);

        if (company == null)
            throw new KeyNotFoundException("Company not found");

        if (company.OwnerId != userId)
            throw new UnauthorizedAccessException("You cannot delete this company");

        _companyRepository.Remove(company);

        await _companyRepository.SaveChangesAsync();
    }

    // GET ALL
    public async Task<List<CompanyResponse>> GetAllAsync()
    {
        var companies = await _companyRepository.GetAllAsync();

        return companies.Select(x => new CompanyResponse
        {
            Id = x.Id,
            Name = x.Name
        }).ToList();
    }

    // GET BY ID
    public async Task<CompanyResponse> GetByIdAsync(int id)
    {
        var company = await _companyRepository.GetByIdAsync(id);

        if (company == null)
            throw new KeyNotFoundException("Company not found");

        return new CompanyResponse
        {
            Id = company.Id,
            Name = company.Name
        };
    }
}