namespace Finik.MainPage.Core.Models
{
    public class News
    {
        public Guid Id { get; set; }

        public required string HeadLine { get; set; }

        public required string Body { get; set; }

        public DateTime CreatedAt { get; set; }

        public Guid Author { get; set; }

        public int? CompanyId { get; set; }

        public int? StockId { get; set; }
    }
}
