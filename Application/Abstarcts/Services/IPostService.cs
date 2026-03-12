using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstarcts.Services;

using Application.Dtos.Post;
using Domain.Entities;

public interface IPostService
{
    Task CreateAsync(CreatePostRequest request, string userId);
    Task UpdateAsync(int id, UpdatePostRequest request, string userId);
    Task DeleteAsync(int id, string userId);
    Task<List<PostResponse>> GetAllAsync();
    Task<PostResponse> GetByIdAsync(int id);
}
