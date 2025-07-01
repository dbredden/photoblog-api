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

    public async Task<PostEntity?> GetByIdAsync(Guid postId)
    {
        return await _context.Posts
            .FirstOrDefaultAsync(p => p.Id == postId);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeletePostAsync(Guid postId)
    {
        var post = await _context.Posts.FirstOrDefaultAsync(x => x.Id == postId);

        if (post is not null)
        {
            _context.Posts.Remove(post);

            return await _context.SaveChangesAsync() > 0;
        }

        return false;
    }
}
