using Microsoft.EntityFrameworkCore;
using PhotoBlog.Application.Interfaces;
using PhotoBlog.Domain.Entities;
using PhotoBlog.Database;

namespace PhotoBlog.Infrastructure.Repositories;

public class PostRepository : IPostRepository
{
    private readonly PostDbContext _context;

    public PostRepository(PostDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(PostEntity post)
    {
        await _context.Posts.AddAsync(post);
        await _context.SaveChangesAsync();
    }

    public async Task<List<PostEntity>> GetAllAsync()
    {
        return await _context.Posts
            .OrderByDescending(p => p.Date)
            .ToListAsync();
    }

    public async Task<PostEntity?> GetByIdAsync(Guid id)
    {
        return await _context.Posts
            .FirstOrDefaultAsync(p => p.Id == id);
    }
}
