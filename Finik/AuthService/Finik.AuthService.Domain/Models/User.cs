namespace Finik.AuthService.Domain.Models;

public class User
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? NickName { get; set; }
    public required string Email { get; set; }

    public required string Password { get; set; }

    public required string Salt { get; set; }

    public required int RoleId { get; set; }
    public required Role Role { get; set; }
}
