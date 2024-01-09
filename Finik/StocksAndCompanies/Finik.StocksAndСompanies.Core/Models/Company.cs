using System.Text.Json.Serialization;

namespace Finik.StockAndCompany.Core.Models;

public class Company
{
    public int Id { get; set; }
    public required string NickName { get; set; }
    public required string FullName { get; set; }
    public String? Inn { get; set; }
    
    [JsonIgnore]
    public List<Stock>? Stocks { get; set; } = new ();
}