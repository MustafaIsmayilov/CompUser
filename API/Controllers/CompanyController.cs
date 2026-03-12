using System.Security.Claims;
using Application.Abstarcts.Services;
using Application.Dtos.Company;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CompanyController : ControllerBase
{
    private readonly ICompanyService _companyService;

    public CompanyController(ICompanyService companyService)
    {
        _companyService = companyService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCompanyRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        await _companyService.CreateAsync(request, userId);

        return StatusCode(201, new { message = "Company created successfully" });
    }

    [Authorize(Roles = "Provider")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateCompanyRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        await _companyService.UpdateAsync(id, request, userId);

        return Ok(new { message = "Company updated successfully" });
    }

    [Authorize(Roles = "Provider")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        await _companyService.DeleteAsync(id, userId);

        return Ok(new { message = "Company deleted successfully" });
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var companies = await _companyService.GetAllAsync();

        return Ok(companies);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var company = await _companyService.GetByIdAsync(id);

        return Ok(company);
    }
}