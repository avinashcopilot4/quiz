using Common.DTO.User;
using Entity.Data;
using Entity.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositories;

public class UserRepository : IUserRepository
{
    private readonly SchoolDbContext _context;

    public UserRepository(SchoolDbContext schoolDbContext)
    {
        _context = schoolDbContext;
    }

    public async Task<IEnumerable<UserDto>> GetUsersAsync()
    {
        return await _context.Users
            .Select(user => new UserDto
            {
                UserId = user.UserId,
                Email = user.Email,
                UserName = user.UserName,
                Role = user.Role,
                IsActive = user.IsActive,
            })
            .ToListAsync();
    }

    public async Task<UserDto?> GetUserByIdAsync(int userId)
    {
        return await _context.Users
            .Where(user => user.UserId == userId)
            .Select(user => new UserDto
            {
                UserId = user.UserId,
                Email = user.Email,
                UserName = user.UserName,
                Role = user.Role,
                IsActive = user.IsActive,
            })
            .FirstOrDefaultAsync();
    }

    public async Task<User> AddUserAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task UpdateUserAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(int userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user != null)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> UserExistsAsync(int userId)
    {
        return await _context.Users.AnyAsync(u => u.UserId == userId);
    }
}
