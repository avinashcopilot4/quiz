using BusinessCore.Interfaces;
using Common.DTO;
using Common.DTO.User;
using Entity.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace QuizApp.Controllers;

[Route("api/users")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

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

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
    {
        var users = await _userService.GetUsersAsync();
        return Ok(users);
    }

    [HttpGet("{userid:int}")]
    public async Task<ActionResult<UserDto>> GetUser(int userid)
    {
        var user = await _userService.GetUserByIdAsync(userid);
        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login([FromBody] LoginRequestDto request)
    {
        if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
        {
            return BadRequest(new { message = "Email and password are required." });
        }

        var normalizedRole = NormalizeRole(request.Role);
        var user = await _userService.AuthenticateUserAsync(request.Email, request.Password);
        if (user == null)
        {
            return Unauthorized(new { message = "Invalid credentials." });
        }

        if (!string.IsNullOrWhiteSpace(request.Role) && !string.Equals(user.ActiveRole, normalizedRole, StringComparison.OrdinalIgnoreCase))
        {
            return Unauthorized(new { message = "Selected role is not assigned to this user." });
        }

        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> CreateUser(User user)
    {
        var created = await _userService.CreateUserAsync(user);
        var userDto = await _userService.GetUserByIdAsync(created.UserId);
        if (userDto == null)
        {
            return StatusCode(500, "Failed to retrieve created user.");
        }

        return CreatedAtAction(nameof(GetUser), new { userid = created.UserId }, userDto);
    }

    [HttpPut("{userid:int}")]
    public async Task<IActionResult> UpdateUser(int userid, User user)
    {
        if (userid != user.UserId)
        {
            return BadRequest();
        }

        if (!await _userService.UserExistsAsync(userid))
        {
            return NotFound();
        }

        await _userService.UpdateUserAsync(user);
        return NoContent();
    }

    [HttpPut("{userid:int}/profile")]
    public async Task<ActionResult<UserDto>> UpdateUserProfile(int userid, UserProfileUpdateDto request)
    {
        if (!await _userService.UserExistsAsync(userid))
        {
            return NotFound();
        }

        var userEntity = await _userService.GetUserEntityByIdAsync(userid);
        if (userEntity == null)
        {
            return NotFound();
        }

        userEntity.UserName = request.Name;
        userEntity.Gender = request.Gender;
        userEntity.PhoneNumber = request.PhoneNumber;
        userEntity.UpdatedDate = DateTime.UtcNow;

        await _userService.UpdateUserAsync(userEntity);

        var updatedUser = await _userService.GetUserByIdAsync(userid);
        if (updatedUser == null)
        {
            return NotFound();
        }

        return Ok(updatedUser);
    }

    [HttpDelete("{userid:int}")]
    public async Task<IActionResult> DeleteUser(int userid)
    {
        if (!await _userService.UserExistsAsync(userid))
        {
            return NotFound();
        }

        await _userService.DeleteUserAsync(userid);
        return NoContent();
    }
}
