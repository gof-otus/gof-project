using Finik.AuthService.Contracts;

namespace Finik.AuthService.Core;

public interface IUserManager
{
    public Task<IReadOnlyList<UserDto>> GetAllUsers();
    public Task<UserDto?> GetUser(Guid id);

    public Task<UserDto?> GetUser(string email);
    public Task<UserDto> CreateUser(UserDto user);
    public Task DeleteUser(Guid id);
    public Task UpdateUser(UserDto user);
}
