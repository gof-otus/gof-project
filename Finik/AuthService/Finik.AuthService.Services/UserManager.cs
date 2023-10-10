using AutoMapper;
using Finik.AuthService.Contracts;
using Finik.AuthService.Core;
using Finik.AuthService.DataAccess;
using Finik.AuthService.Domain.Models;
using Finik.AuthService.Services.Exceptions;

namespace Finik.AuthService.Services;

public class UserManager : IUserManager
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    public UserManager(IUserRepository usersRepository, IMapper mapper)
    {
        _userRepository = usersRepository;
        _mapper = mapper;
    }
    public async Task<UserDto> CreateUser(UserDto user)
    {
        var userModel = _mapper.Map<User>(user);
        var existedUser = await _userRepository.GetUser(user.Id);
        if (existedUser == null)
        {
            await _userRepository.CreateUser(userModel);
            return _mapper.Map<UserDto>(userModel);
        }
        throw new UserException($"User with id {user.Id} already exists");
    }

    public async Task DeleteUser(Guid id) => await _userRepository.DeleteUser(id);

    public async Task<IReadOnlyList<UserDto>> GetAllUsers()
    {
        var allUsers = await _userRepository.GetAllUsers();
        return _mapper.Map<IReadOnlyList<UserDto>>(allUsers);
    }

    public async Task<UserDto?> GetUser(Guid id)
    {
        var userModel = await _userRepository.GetUser(id);
        return _mapper.Map<UserDto>(userModel);
    }

    public async Task<UserDto?> GetUser(string email)
    {
        var userModel = await _userRepository.GetUser(email);
        return _mapper.Map<UserDto>(userModel);
    }

    public async Task UpdateUser(UserDto user)
    {
        var userModel = _mapper.Map<User>(user);
        await _userRepository.UpdateUser(userModel);
    }
}