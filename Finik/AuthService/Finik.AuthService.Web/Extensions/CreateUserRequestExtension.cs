using Finik.AuthService.Contracts;
using Finik.AuthService.Web.Models;

namespace Finik.AuthService.Web.Extensions
{
    internal static class CreateUserRequestExtension
    {
        public static UserDto ToDto(this CreateUserRequest createUserRequest, HashedPassword hashedPassword) => new()
        {
            Id = Guid.NewGuid(),
            Email = createUserRequest.Email,
            FirstName = createUserRequest.FirstName,
            HashedPassword = hashedPassword,
            LastName = createUserRequest.LastName,
            NickName = createUserRequest.NickName,
            Role = createUserRequest.Role   
        };
    }
}
