namespace Finik.Contracts;

public class NewsDto
{
    public int NewsId { get; set; }

    public required string HeadLine { get; set; }

    public required string Body { get; set; }

    public DateTime CreatedDate { get; set; }

    public int AuthorId { get; set; }
    public required UserDto Author { get; set; }

    public bool IsPublished { get; set; }

    public DateTime PublishDate { get; set; }
}