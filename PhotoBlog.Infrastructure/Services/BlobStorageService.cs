
using Azure.Storage.Blobs;

namespace PhotoBlog.Infrastructure.Services;

public class BlobStorageService
{
    private readonly BlobContainerClient _containerClient;

    //public BlobStorageService(IConfiguration config)
    //{
    //    var connectionString = config["AzureBlob:ConnectionString"];
    //    var containerName = config["AzureBlob:Container"];
    //    _containerClient = new BlobContainerClient(connectionString, containerName);
    //}

    //public async Task<string> UploadAsync(IFormFile file)
    //{
    //    var blobClient = _containerClient.GetBlobClient(file.FileName);
    //    await using var stream = file.OpenReadStream();
    //    await blobClient.UploadAsync(stream, overwrite: true);
    //    return blobClient.Uri.ToString();
    //}
}
