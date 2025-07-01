
using MediatR;
using PhotoBlog.Application.Common.DTOs;
using PhotoBlog.Application.Interfaces;
using PhotoBlog.Domain.Entities;

namespace PhotoBlog.Application.Commands;

public class PatchPostCommand : IRequest<PostEntity>
{
    public Guid PostId { get; init; }
    public PostPatchDto PatchDto { get; init; }
}

public class PatchPostCommandHandler : IRequestHandler<PatchPostCommand, PostEntity>
{
    private readonly IPostRepository _repository;

    public PatchPostCommandHandler(IPostRepository repository)
    {
        _repository = repository;
    }

    public async Task<PostEntity> Handle(PatchPostCommand request, CancellationToken cancellationToken)
    {
        var post = await _repository.GetByIdAsync(request.PostId);
        if (post == null)
        {
            throw new KeyNotFoundException($"Post with ID {request.PostId} not found.");
        }

        // Update only the fields that are not null in the DTO
        if (request.PatchDto.Description != null)
            post.Description = request.PatchDto.Description;

        if (request.PatchDto.Location != null)
            post.Location = request.PatchDto.Location;

        if (request.PatchDto.Date.HasValue)
            post.Date = request.PatchDto.Date.Value;

        await _repository.SaveChangesAsync();

        return post;
    }
}
