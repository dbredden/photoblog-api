namespace PhotoBlog.Application.DTOs;

public class PostSummaryDto
{
    public Guid Id { get; set; }
    public string PhotoWebUrl { get; set; }
    public string Location { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
}
