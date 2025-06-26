using PhotoBlog.Domain.Entities;

namespace PhotoBlog.Application.Interfaces;

public interface IPostRepository
{
    Task AddAsync(PostEntity post);
    Task<List<PostEntity>> GetAllAsync();
    Task<PostEntity?> GetByIdAsync(Guid id);
    Task<PostEntity> UpdatePostAsync(Guid postId, PostEntity entity);
    Task<bool> DeletePostAsync(Guid postId);

}
