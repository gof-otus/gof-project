namespace Finik.StockAndCompany.Core.Models;

public class Company
{
    public int Id { get; set; }
    public required string NickName { get; set; }
    public required string FullName { get; set; }
    public int Inn { get; set; }
    public string? WebSite { get; set; }
    public DateTime? DeleteDate { get; set; }
    public List<Stock>? Stocks { get; } = new List<Stock>();
}