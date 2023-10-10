namespace Finik.NewsService.Models;

public class News
{
    public Guid Id { get; set; }

    public required string HeadLine { get; set; }

    public required string Body { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid Author { get; set; }

    public bool IsPublished { get; set; }

    public DateTime PublishedAt { get; set; }
}
