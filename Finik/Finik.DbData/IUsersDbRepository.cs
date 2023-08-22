using Finik.Models;

namespace Finik.DbData;

public interface IUsersDbRepository
{
    public Task<IReadOnlyList<User>> GetAllUsers();
    public Task<User?> GetUser(int id);
    public Task<User> CreateUser(User user);
    public Task DeleteUser(int id);
    public Task UpdateUser(User user);
}
