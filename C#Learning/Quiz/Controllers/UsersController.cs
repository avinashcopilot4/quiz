using BusinessCore.Interfaces;
using Common.DTO.User;
using Entity.Models;
using Microsoft.AspNetCore.Mvc;

[Route("api/users")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

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

    [HttpPost]
    public async Task<ActionResult<UserDto>> CreateUser(User user)
    {
        var created = await _userService.CreateUserAsync(user);
        var userDto = new UserDto
        {
            UserId = created.UserId,
            UserName = created.UserName,
            Email = created.Email,
            Role = created.Role,
            IsActive = created.IsActive,
        };

        return CreatedAtAction(nameof(GetUser), new { userid = userDto.UserId }, userDto);
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
