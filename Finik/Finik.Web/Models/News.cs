namespace WebApplication1.Models
{
    public class News
    {
        public string Id { get; set; }

        public string HeadLine { get; set; }

        public string Body { get; set; }

        public DateTime CreatedAt { get; set; }

        public int AuthorId { get; set; }
        public String Author { get; set; }

        public bool IsPublished { get; set; }

        public DateTime PublishedAt { get; set; }
    }
}
