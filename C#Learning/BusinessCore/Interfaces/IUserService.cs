using Common.DTO.User;
using Entity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessCore.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetUsersAsync();
    Task<UserDto?> GetUserByIdAsync(int userId);
    Task<User> CreateUserAsync(User user);
    Task UpdateUserAsync(User user);
    Task DeleteUserAsync(int userId);
    Task<bool> UserExistsAsync(int userId);
}
