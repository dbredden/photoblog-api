using MediatR;
using Microsoft.AspNetCore.Http;
using PhotoBlog.Application.Interfacecs;
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

    public CreatePostCommandHandler(IBlobStorageService blobService)
    {
        _blobService = blobService;
    }

    public async Task<Guid> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var urls = await _blobService.UploadOriginalAsync(request.Image);

        var post = new PostEntity
        {
            Id = Guid.NewGuid(),
            PhotoOriginalUrl = urls.Original,
            //PhotoWebUrl = urls.Web,
            //PhotoThumbnailUrl = urls.Thumbnail,
            Location = request.Location,
            Description = request.Description,
            Date = request.Date
        };

        await _repository.AddAsync(post);
        return post.Id;
    }
}

