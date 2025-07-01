
namespace PhotoBlog.Application.Common.DTOs;

public class PostPatchDto
{
    public string? Description { get; set; }
    public string? Location { get; set; }
    public DateTime? Date { get; set; }
}
