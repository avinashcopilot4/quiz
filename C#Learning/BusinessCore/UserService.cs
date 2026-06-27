using BusinessCore.Interfaces;
using Common.DTO.User;
using Entity.Models;
using Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessCore;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public Task<IEnumerable<UserDto>> GetUsersAsync()
    {
        return _userRepository.GetUsersAsync();
    }

    public Task<UserDto?> GetUserByIdAsync(int userId)
    {
        return _userRepository.GetUserByIdAsync(userId);
    }

    public Task<User> CreateUserAsync(User user)
    {
        return _userRepository.AddUserAsync(user);
    }

    public Task UpdateUserAsync(User user)
    {
        return _userRepository.UpdateUserAsync(user);
    }

    public Task DeleteUserAsync(int userId)
    {
        return _userRepository.DeleteUserAsync(userId);
    }

    public Task<bool> UserExistsAsync(int userId)
    {
        return _userRepository.UserExistsAsync(userId);
    }
}
