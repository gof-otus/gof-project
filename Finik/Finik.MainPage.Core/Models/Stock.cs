namespace Finik.MainPage.Core.Models
{
    public class Stock
    {
        public int Id { get; set; }
        public String? Type { get; set; }
        public String? Isin { get; set; }
        public String? TradeCode { get; set; }
        public String? Category { get; set; }
        public required int CompanyId { get; set; }
    }
}