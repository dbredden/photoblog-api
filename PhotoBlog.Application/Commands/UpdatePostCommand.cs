
using MediatR;
using PhotoBlog.Application.Interfaces;
using PhotoBlog.Domain.Entities;

namespace PhotoBlog.Application.Commands;

public record UpdatePostCommand(Guid postId, PostEntity post) : IRequest<PostEntity>;

public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, PostEntity>
{
    private readonly IPostRepository _repository;

    public UpdatePostCommandHandler(IPostRepository repository)
    {
        _repository = repository;
    }

    public async Task<PostEntity> Handle(UpdatePostCommand request, CancellationToken cancelaltionToken)
    {
        return await _repository.UpdatePostAsync(request.postId, request.post);
    }
}
