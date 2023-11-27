using Finik.AuthService.Contracts;

namespace Finik.AuthService.Web.Models
{
    public class CreateUserRequest
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? NickName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required Role Role { get; set; }
    }
}
