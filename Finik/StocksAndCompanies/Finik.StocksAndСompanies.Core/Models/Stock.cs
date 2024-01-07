namespace Finik.StockAndCompany.Core.Models;

public class Stock
{
    public int Id { get; set; }
    public String? Type { get; set; }
    public String? Isin { get; set; }
    public String? TradeCode { get; set; }
    public String? Category { get; set; }
    public int CompanyId { get; set; }
    public required Company Company { get; set; }
}