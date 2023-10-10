using Finik.AuthService.Domain.Models;

namespace Finik.AuthService.DataAccess;

public interface IUserRepository
{
    public Task<IEnumerable<User>> GetAllUsers();
    public Task<User?> GetUser(Guid id);
    public Task<User?> GetUser(string email);
    public Task<User> CreateUser(User user);
    public Task DeleteUser(Guid id);
    public Task UpdateUser(User user);
}
