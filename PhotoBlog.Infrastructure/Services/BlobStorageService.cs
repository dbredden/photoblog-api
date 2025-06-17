using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using PhotoBlog.Application.DTOs;
using PhotoBlog.Application.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
    

namespace PhotoBlog.Infrastructure.Services;

public class BlobStorageService : IBlobStorageService
{
    private readonly BlobContainerClient _containerClient;

    public BlobStorageService(IConfiguration config)
    {
        var connectionString = config["AzureBlob:ConnectionString"];
        var containerName = config["AzureBlob:Container"];
        _containerClient = new BlobContainerClient(connectionString, containerName);
    }

    public async Task<UploadedImageUrls> UploadAndProcessImageAsync(IFormFile file)
    {
        var extension = Path.GetExtension(file.FileName);
        var baseName = Guid.NewGuid().ToString();
        var originalPath = $"original/{baseName}{extension}";
        var webPath = $"web/{baseName}.jpg";
        var thumbPath = $"thumb/{baseName}.jpg";

        await using var inputStream = file.OpenReadStream();
        using var image = await Image.LoadAsync(inputStream); // from SixLabors.ImageSharp

        // Create and upload original
        var originalBlob = _containerClient.GetBlobClient(originalPath);
        await using (var originalStream = file.OpenReadStream())
        {
            await originalBlob.UploadAsync(originalStream, overwrite: true);
        }

        // Create and upload web size
        var webBlob = _containerClient.GetBlobClient(webPath);
        using (var webStream = new MemoryStream())
        {
            image.Mutate(x => x.Resize(new ResizeOptions
            {
                Mode = ResizeMode.Max,
                Size = new Size(1600, 0)
            }));

            await image.SaveAsJpegAsync(webStream);
            webStream.Position = 0;
            await webBlob.UploadAsync(webStream, overwrite: true);
        }

        // Re-load the image again to reset size for thumbnail
        inputStream.Position = 0;
        using var thumbImage = await Image.LoadAsync(inputStream);
        var thumbBlob = _containerClient.GetBlobClient(thumbPath);
        using var thumbStream = new MemoryStream();
        thumbImage.Mutate(x => x.Resize(new ResizeOptions
        {
            Mode = ResizeMode.Crop,
            Size = new Size(400, 400)
        }));

        await thumbImage.SaveAsJpegAsync(thumbStream);
        thumbStream.Position = 0;
        await thumbBlob.UploadAsync(thumbStream, overwrite: true);

        return new UploadedImageUrls
        {
            OriginalUrl = originalBlob.Uri.ToString(),
            WebUrl = webBlob.Uri.ToString(),
            ThumbnailUrl = thumbBlob.Uri.ToString()
        };
    }
}
