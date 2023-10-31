namespace Finik.StockAndCompany.Core.Models;

public class Stock
{
    public int Id { get; set; }
    public int Type { get; set; }
    public int Isin { get; set; }
    public int TradeCode { get; set; }
    public int Category { get; set; }
    public int CompanyId { get; set; }
    public DateTime? DeleteDate { get; set; }
    public required Company Company { get; set; }
}