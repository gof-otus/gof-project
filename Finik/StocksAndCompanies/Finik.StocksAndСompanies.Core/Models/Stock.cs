using System.Text.Json.Serialization;

namespace Finik.StockAndCompany.Core.Models;

public class Stock
{
    public int Id { get; set; }
    public String? Type { get; set; }
    public String? Isin { get; set; }
    public String? TradeCode { get; set; }
    public String? Category { get; set; }
    public required int CompanyId { get; set; }

    [JsonIgnore]
    public Company Company { get; set; }
}