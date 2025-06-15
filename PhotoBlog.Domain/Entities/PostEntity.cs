

namespace PhotoBlog.Domain.Entities
{
    public class PostEntity
    {
        public Guid Id { get; set; }
        public string PhotoOriginalUrl { get; set; }
        public string PhotoWebUrl { get; set; }
        public string PhotoThumbnailUrl { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}
