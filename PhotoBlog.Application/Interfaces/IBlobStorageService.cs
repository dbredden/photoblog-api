
using Microsoft.AspNetCore.Http;

namespace PhotoBlog.Application.Interfacecs;

public interface IBlobStorageService
{
    Task<string> UploadOriginalAsync(IFormFile file);

}
