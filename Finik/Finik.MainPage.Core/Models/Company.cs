namespace Finik.MainPage.Core.Models
{
    public class Company
    {
        public int Id { get; set; }
        public required string NickName { get; set; }
        public required string FullName { get; set; }
        public String? Inn { get; set; }
    }
}