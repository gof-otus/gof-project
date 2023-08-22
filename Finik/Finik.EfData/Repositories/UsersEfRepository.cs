using Finik.DbData;
using Finik.Models;
using Microsoft.EntityFrameworkCore;

namespace Finik.Data.Repositories;

public class UsersEfRepository : IUsersDbRepository
{
    private readonly FinikDbContext _dbContext;
    public UsersEfRepository(FinikDbContext dbContext)
    {
        _dbContext = dbContext; 
    }
    public async Task<User> CreateUser(User users)
    {
        var newValue = await _dbContext.Users.AddAsync(users);
        await _dbContext.SaveChangesAsync();
        return newValue.Entity;
    }

    public async Task DeleteUser(int id)
    {
        var userToDelete = await _dbContext.Users.SingleOrDefaultAsync(n => n.Id == id);
        if (userToDelete is not null)
        {
            _dbContext.Users.Remove(userToDelete);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<IReadOnlyList<User>> GetAllUsers() => await _dbContext.Users.ToListAsync();

    public async Task<User?> GetUser(int id) => await _dbContext.Users.SingleOrDefaultAsync(n => n.Id == id);


    public async Task UpdateUser(User user)
    {
        var userToUpdate = await _dbContext.Users.SingleOrDefaultAsync(n => n.Id == user.Id);
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync();
    }
}
