using Microsoft.AspNetCore.Http;
using PhotoBlog.Application.DTOs;

namespace PhotoBlog.Application.Interfaces;

public interface IBlobStorageService
{
    Task<UploadedImageUrls> UploadAndProcessImageAsync(IFormFile file);

}
