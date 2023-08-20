namespace Finik.Models;

public class News
{
    public int NewsId { get; set; }

    public required string HeadLine { get; set; }

    public required string Body { get; set; }

    public DateTime CreatedDate { get; set; }

    public int AuthorId { get; set; }
    public required User Author { get; set; }

    public bool IsPublished { get; set; } 

    public DateTime PublishDate { get; set; }
}
