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

    private static string NormalizeRole(string? role)
    {
        if (string.IsNullOrWhiteSpace(role))
        {
            return "employee";
        }

        return role.Trim().ToLowerInvariant() switch
        {
            "admin" => "admin",
            "employee" => "employee",
            _ => "employee"
        };
    }

    public async Task<IEnumerable<UserDto>> GetUsersAsync()
    {
        var users = await _context.Users
            .Include(u => u.UserRoles)
            .ToListAsync();

        return users.Select(user => new UserDto
        {
            Id = user.UserId.ToString(),
            Name = user.UserName,
            Email = user.Email,
            Roles = user.UserRoles.Select(ur => NormalizeRole(ur.RoleName)).Distinct().ToList(),
            ActiveRole = NormalizeRole(user.UserRoles.FirstOrDefault()?.RoleName) ?? "employee",
            Gender = user.Gender,
            PhoneNumber = user.PhoneNumber,
        }).ToList();
    }

    public async Task<UserDto?> GetUserByIdAsync(int userId)
    {
        var user = await _context.Users
            .Include(u => u.UserRoles)
            .FirstOrDefaultAsync(u => u.UserId == userId);

        if (user == null)
        {
            return null;
        }

        return new UserDto
        {
            Id = user.UserId.ToString(),
            Name = user.UserName,
            Email = user.Email,
            Roles = user.UserRoles.Select(ur => NormalizeRole(ur.RoleName)).Distinct().ToList(),
            ActiveRole = NormalizeRole(user.UserRoles.FirstOrDefault()?.RoleName) ?? "employee",
            Gender = user.Gender,
            PhoneNumber = user.PhoneNumber,
        };
    }

    public async Task<UserDto?> AuthenticateUserAsync(string email, string password)
    {
        var user = await _context.Users
            .Include(u => u.UserRoles)
            .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower() && u.Password == password && u.IsActive);

        if (user == null)
        {
            return null;
        }

        return new UserDto
        {
            Id = user.UserId.ToString(),
            Name = user.UserName,
            Email = user.Email,
            Roles = user.UserRoles.Select(ur => NormalizeRole(ur.RoleName)).Distinct().ToList(),
            ActiveRole = NormalizeRole(user.UserRoles.FirstOrDefault()?.RoleName) ?? "employee",
            Gender = user.Gender,
            PhoneNumber = user.PhoneNumber,
        };
    }

    public async Task<User> AddUserAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User?> GetUserEntityByIdAsync(int userId)
    {
        return await _context.Users.FindAsync(userId);
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
