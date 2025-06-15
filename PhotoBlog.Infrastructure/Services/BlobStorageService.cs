
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using PhotoBlog.Application.Interfacecs;

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

    public async Task<string> UploadOriginalAsync(IFormFile file)
    {
        var extension = Path.GetExtension(file.FileName);
        var uniqueName = $"{Guid.NewGuid()}{extension}";
        var blobPath = $"original/{uniqueName}";

        var blobClient = _containerClient.GetBlobClient(blobPath);
        await using var stream = file.OpenReadStream();
        await blobClient.UploadAsync(stream, overwrite: true);
        return blobClient.Uri.ToString();
    }
}
