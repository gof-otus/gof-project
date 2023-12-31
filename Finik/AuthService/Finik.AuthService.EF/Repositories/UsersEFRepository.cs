﻿using Finik.AuthService.DataAccess;
using Finik.AuthService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Finik.AuthService.EF.Repositories;

public class UsersEfRepository : IUserRepository
{
    private readonly AuthDbContext _dbContext;
    public UsersEfRepository(AuthDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<User> CreateUser(User user)
    {
        var role = _dbContext.Roles.SingleOrDefault(r => r.Id == user.Role.Id);
        user.Role = role ?? throw new ArgumentNullException("Role not found in db");

        var newValue = await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
        return newValue.Entity;
    }

    public async Task DeleteUser(Guid id)
    {
        var userToDelete = await _dbContext.Users.Include(u => u.Role).SingleOrDefaultAsync(n => n.Id == id);
        if (userToDelete is not null)
        {
            _dbContext.Users.Remove(userToDelete);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<User>> GetAllUsers() 
        => await _dbContext.Users.Include(u => u.Role).ToListAsync();

    public async Task<User?> GetUser(string email) 
        => await _dbContext.Users.Include(u => u.Role).SingleOrDefaultAsync(u => u.Email == email);

    public async Task<User?> GetUser(Guid id) 
        => await _dbContext.Users.Include(u => u.Role).SingleOrDefaultAsync(u => u.Id == id);

    public async Task UpdateUser(User user)
    {
        var userToUpdate = await _dbContext.Users.SingleOrDefaultAsync(n => n.Id == user.Id);
        if (userToUpdate is not null)
        {
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
        }      
    }
}