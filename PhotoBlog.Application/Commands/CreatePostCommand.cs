using MediatR;
using Microsoft.AspNetCore.Http;
using PhotoBlog.Application.Interfaces;
using PhotoBlog.Domain.Entities;


namespace PhotoBlog.Application.Commands;

public class CreatePostCommand : IRequest<Guid>
{
    public IFormFile Image { get; init; }
    public string Location { get; init; }
    public string Description { get; init; }
    public DateTime Date { get; init; }
}

public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, Guid>
{
    private readonly IBlobStorageService _blobService;
    private readonly IPostRepository _repository;

    public CreatePostCommandHandler(IBlobStorageService blobService, IPostRepository repository)
    {
        _blobService = blobService;
        _repository = repository;
    }

    public async Task<Guid> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var urls = await _blobService.UploadAndProcessImageAsync(request.Image);

        var post = new PostEntity
        {
            Id = Guid.NewGuid(),
            PhotoOriginalUrl = urls.OriginalUrl,
            PhotoWebUrl = urls.WebUrl,
            PhotoThumbnailUrl = urls.ThumbnailUrl,
            Location = request.Location,
            Description = request.Description,
            Date = request.Date
        };

        await _repository.AddAsync(post);
        return post.Id;
    }
}

