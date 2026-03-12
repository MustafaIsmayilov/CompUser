using Application.Abstarcts.Repositories;
using Application.Abstarcts.Services;
using Application.Dtos.Post;
using Domain.Entities;

namespace Persistence.Services;

public class PostService : IPostService
{
    private readonly IPostRepository _postRepository;
    private readonly IServiceRepository _serviceRepository;
    private readonly ICompanyRepository _companyRepository;

    public PostService(
        IPostRepository postRepository,
        IServiceRepository serviceRepository,
        ICompanyRepository companyRepository)
    {
        _postRepository = postRepository;
        _serviceRepository = serviceRepository;
        _companyRepository = companyRepository;
    }

    public async Task CreateAsync(CreatePostRequest request, string userId)
    {
        var service = await _serviceRepository.GetByIdAsync(request.ServiceId);

        if (service == null)
            throw new KeyNotFoundException("Service not found");

        var company = await _companyRepository.GetByIdAsync(service.CompanyId);

        if (company.OwnerId != userId)
            throw new UnauthorizedAccessException("You cannot add post to this service");

        var post = new Post
        {
            Content = request.Content,
            ServiceId = request.ServiceId
        };

        await _postRepository.AddAsync(post);
        await _postRepository.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, UpdatePostRequest request, string userId)
    {
        var post = await _postRepository.GetByIdAsync(id);

        if (post == null)
            throw new KeyNotFoundException("Post not found");

        var service = await _serviceRepository.GetByIdAsync(post.ServiceId);

        var company = await _companyRepository.GetByIdAsync(service.CompanyId);

        if (company.OwnerId != userId)
            throw new UnauthorizedAccessException("You cannot update this post");

        post.Content = request.Content;

        _postRepository.Update(post);
        await _postRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id, string userId)
    {
        var post = await _postRepository.GetByIdAsync(id);

        if (post == null)
            throw new KeyNotFoundException("Post not found");

        var service = await _serviceRepository.GetByIdAsync(post.ServiceId);

        var company = await _companyRepository.GetByIdAsync(service.CompanyId);

        if (company.OwnerId != userId)
            throw new UnauthorizedAccessException("You cannot delete this post");

        _postRepository.Remove(post);
        await _postRepository.SaveChangesAsync();
    }

    public async Task<List<PostResponse>> GetAllAsync()
    {
        var posts = await _postRepository.GetAllAsync();

        return posts.Select(x => new PostResponse
        {
            Id = x.Id,
            Content = x.Content,
            ServiceId = x.ServiceId
        }).ToList();
    }

    public async Task<PostResponse> GetByIdAsync(int id)
    {
        var post = await _postRepository.GetByIdAsync(id);

        if (post == null)
            throw new KeyNotFoundException("Post not found");

        return new PostResponse
        {
            Id = post.Id,
            Content = post.Content,
            ServiceId = post.ServiceId
        };
    }
}