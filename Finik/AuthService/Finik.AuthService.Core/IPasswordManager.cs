using Finik.AuthService.Contracts;

namespace Finik.AuthService.Core
{
    public interface IPasswordManager
    {
        HashedPassword HashPassword(string password);
        bool IsPasswordValid(string password, HashedPassword hashedPassword);
    }
}
