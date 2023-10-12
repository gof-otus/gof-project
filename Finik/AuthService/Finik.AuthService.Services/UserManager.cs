using AutoMapper;
using Finik.AuthService.Contracts;
using Finik.AuthService.Core;
using Finik.AuthService.DataAccess;
using Finik.AuthService.Domain.Models;
using Finik.AuthService.Services.Exceptions;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Finik.AuthService.Services;

public class UserManager : IUserManager
{
    private readonly IDistributedCache _userCache;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    public UserManager(IDistributedCache userCache, IUserRepository userRepository, IMapper mapper)
    {
        _userCache = userCache;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserDto> CreateUser(UserDto user)
    {
        var userModel = _mapper.Map<User>(user);

        if (await _userRepository.GetUser(user.Id) is not null)
        {
            throw new UserException($"User with id {user.Id} already exists");
        }

        if (await _userRepository.GetUser(user.Email) is not null)
        {
            throw new UserException($"User with email {user.Email} already exists");
        }

        await _userRepository.CreateUser(userModel);

        var createdUser = _mapper.Map<UserDto>(userModel);

        await _userCache.SetAsync(createdUser.Id.ToString(), JsonSerializer.SerializeToUtf8Bytes(createdUser));

        return createdUser;
    }

    public async Task DeleteUser(Guid id)
    {
        await _userRepository.DeleteUser(id);
        await _userCache.RemoveAsync(id.ToString());
    }

    public async Task<IReadOnlyList<UserDto>> GetAllUsers()
    {
        var allUsers = await _userRepository.GetAllUsers();
        return _mapper.Map<IReadOnlyList<UserDto>>(allUsers);
    }

    public async Task<UserDto?> GetUser(Guid id)
    {
        UserDto? user = null;

        var cachedUser = await _userCache.GetAsync(id.ToString());

        if (cachedUser is not null) 
        { 
            user = JsonSerializer.Deserialize<UserDto>(cachedUser); 
        }

        if (user is null)
        {
            var userEntity = await _userRepository.GetUser(id);
            user = _mapper.Map<UserDto>(userEntity);

            if (user is not null)
            {
                await _userCache.SetAsync(user.Id.ToString(), JsonSerializer.SerializeToUtf8Bytes(user));
            }
        }

        return user;
    }

    public async Task UpdateUser(UserDto user)
    {
        if (await _userRepository.GetUser(user.Id) is null)
        {
            throw new UserException($"User with id {user.Id} not found");
        }
        var userModel = _mapper.Map<User>(user);
        await _userRepository.UpdateUser(userModel);
        await _userCache.SetAsync(user.Id.ToString(), JsonSerializer.SerializeToUtf8Bytes(user));
    }
}