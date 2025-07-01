using PhotoBlog.Domain.Entities;

namespace PhotoBlog.Application.Interfaces;

public interface IPostRepository
{
    Task AddAsync(PostEntity post);
    Task<List<PostEntity>> GetAllAsync();
    Task<PostEntity?> GetByIdAsync(Guid id);
    Task SaveChangesAsync();
    Task<bool> DeletePostAsync(Guid postId);

}
