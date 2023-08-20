using Finik.DbData;
using Finik.Models;
using Microsoft.EntityFrameworkCore;

namespace Finik.Data.Repositories;

public class UsersEfRepository : IUsersDbRepository
{
    private readonly DbContext _dbContext;
    public UsersEfRepository(DbContext dbContext)
    {
        _dbContext = dbContext; 
    }
    public async Task<User> CreateUserAsync(User users)
    {
        var newValue = await _dbContext.Set<User>().AddAsync(users);
        await _dbContext.SaveChangesAsync();
        return newValue.Entity;
    }

    public async Task DeleteUserAsync(int id)
    {
        var userToDelete = await _dbContext.Set<User>().SingleOrDefaultAsync(n => n.UserId == id);
        if (userToDelete is not null)
        {
            _dbContext.Set<User>().Remove(userToDelete);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync() => await _dbContext.Set<User>().ToListAsync();

    public async Task<User?> GetUserAsync(int id) => await _dbContext.Set<User>().SingleOrDefaultAsync(n => n.UserId == id);


    public async Task UpdateUserAsync(User user)
    {
        var userToUpdate = await _dbContext.Set<User>().SingleOrDefaultAsync(n => n.UserId == user.UserId);
        _dbContext.Set<User>().Update(user);
        await _dbContext.SaveChangesAsync();
    }
}
