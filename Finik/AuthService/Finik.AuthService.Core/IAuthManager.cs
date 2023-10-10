using Finik.AuthService.Contracts;

namespace Finik.AuthService.Core;

public interface IAuthManager
{
    public string GenerateToken(UserDto user);
}
