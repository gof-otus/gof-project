namespace Finik.Models;

public class User
{
    public int UserId { get; set; } 
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? NickName { get; set; }
    public required string Email { get; set; }
}
