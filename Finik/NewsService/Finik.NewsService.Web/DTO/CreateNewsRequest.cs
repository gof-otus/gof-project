namespace Finik.NewsService.Web.DTO
{
    public class CreateNewsRequest
    {
        public required string HeadLine { get; set; }

        public required string Body { get; set; }
    }
}
