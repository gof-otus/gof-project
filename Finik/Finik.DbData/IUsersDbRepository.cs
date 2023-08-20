using Finik.Models;

namespace Finik.DbData;

public interface IUsersDbRepository
{
    public Task<IEnumerable<User>> GetAllUsersAsync();
    public Task<User?> GetUserAsync(int id);
    public Task<User> CreateUserAsync(User user);
    public Task DeleteUserAsync(int id);
    public Task UpdateUserAsync(User user);
}
