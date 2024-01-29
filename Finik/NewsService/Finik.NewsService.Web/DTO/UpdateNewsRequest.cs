namespace Finik.NewsService.Web.DTO;

public class UpdateNewsRequest
{
    public required string HeadLine { get; set; }

    public required string Body { get; set; }

    public required int? Companyid { get; set; }

    public required int? StockId { get; set; }
}
