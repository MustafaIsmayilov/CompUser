using Application.Abstarcts.Repositories;
using Application.Abstarcts.Services;
using Application.Dtos.Service;
using Domain.Entities;

namespace Persistence.Services;

public class ServiceService : IServiceService
{
    private readonly IServiceRepository _serviceRepository;
    private readonly ICompanyRepository _companyRepository;

    public ServiceService(
        IServiceRepository serviceRepository,
        ICompanyRepository companyRepository)
    {
        _serviceRepository = serviceRepository;
        _companyRepository = companyRepository;
    }

    public async Task CreateAsync(CreateServiceRequest request, string userId)
    {
        var company = await _companyRepository.GetByIdAsync(request.CompanyId);

        if (company == null)
            throw new KeyNotFoundException("Company not found");

        if (company.OwnerId != userId)
            throw new UnauthorizedAccessException("You cannot add service to this company");

        var service = new Service
        {
            Name = request.Name,
            Description = request.Description,
            CompanyId = request.CompanyId
        };

        await _serviceRepository.AddAsync(service);
        await _serviceRepository.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, UpdateServiceRequest request, string userId)
    {
        var service = await _serviceRepository.GetByIdAsync(id);

        if (service == null)
            throw new KeyNotFoundException("Service not found");

        var company = await _companyRepository.GetByIdAsync(service.CompanyId);

        if (company.OwnerId != userId)
            throw new UnauthorizedAccessException("You cannot update this service");

        service.Name = request.Name;
        service.Description = request.Description;

        _serviceRepository.Update(service);
        await _serviceRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id, string userId)
    {
        var service = await _serviceRepository.GetByIdAsync(id);

        if (service == null)
            throw new KeyNotFoundException("Service not found");

        var company = await _companyRepository.GetByIdAsync(service.CompanyId);

        if (company.OwnerId != userId)
            throw new UnauthorizedAccessException("You cannot delete this service");

        _serviceRepository.Remove(service);
        await _serviceRepository.SaveChangesAsync();
    }

    public async Task<List<ServiceResponse>> GetAllAsync()
    {
        var services = await _serviceRepository.GetAllAsync();

        return services.Select(x => new ServiceResponse
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description
        }).ToList();
    }

    public async Task<ServiceResponse> GetByIdAsync(int id)
    {
        var service = await _serviceRepository.GetByIdAsync(id);

        if (service == null)
            throw new KeyNotFoundException("Service not found");

        return new ServiceResponse
        {
            Id = service.Id,
            Name = service.Name,
            Description = service.Description
        };
    }
}