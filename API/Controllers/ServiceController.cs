using System.Security.Claims;
using Application.Abstarcts.Services;
using Application.Dtos.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ServiceController : ControllerBase
{
    private readonly IServiceService _serviceService;

    public ServiceController(IServiceService serviceService)
    {
        _serviceService = serviceService;
    }

    [Authorize(Roles = "Provider")]
    [HttpPost]
    public async Task<IActionResult> Create(CreateServiceRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        await _serviceService.CreateAsync(request, userId);

        return StatusCode(201, new { message = "Service created successfully" });
    }

    [Authorize(Roles = "Provider")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateServiceRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        await _serviceService.UpdateAsync(id, request, userId);

        return Ok(new { message = "Service updated successfully" });
    }

    [Authorize(Roles = "Provider")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        await _serviceService.DeleteAsync(id, userId);

        return Ok(new { message = "Service deleted successfully" });
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var services = await _serviceService.GetAllAsync();

        return Ok(services);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var service = await _serviceService.GetByIdAsync(id);

        return Ok(service);
    }
}