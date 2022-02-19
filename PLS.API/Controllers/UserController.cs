using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PLS.Entities.ConstTypes;
using PLS.Entities.Dtos;
using PLS.Services.Abstract;

namespace PLS.API.Controllers;

[Route("api/users")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [Authorize(Roles = RoleTypes.Admins)]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetUser([FromRoute] int id)
    {
        var user = await _userService.GetAsync(id);
        return Ok(user);
    }

    [Authorize(Roles = RoleTypes.All)]
    [HttpGet("get-current-user")]
    public async Task<IActionResult> GetCurrentUser()
    {
        var userName = User?.Identity?.Name;

        if (string.IsNullOrEmpty(userName)) return BadRequest("User is not logged in.");

        var user = await _userService.GetCurrentUserAsync(userName);
        return Ok(user);
    }

    [Authorize(Roles = RoleTypes.Admins)]
    [HttpGet]
    public async Task<IActionResult> GetAllUsers(
        [FromQuery(Name = "isDeleted")] bool isDeleted = false,
        [FromQuery(Name = "isActive")] bool isActive = true)
    {
        var users = await _userService.GetAllAsync(isDeleted, isActive);

        return Ok(users);
    }

    [Authorize(Roles = RoleTypes.Admins)]
    [HttpGet("get-user-count")]
    public async Task<IActionResult> GetUserCount(
        [FromQuery(Name = "isDeleted")] bool isDeleted = false,
        [FromQuery(Name = "isActive")] bool isActive = true)
    {
        var count = await _userService.GetUserCountAsync(isDeleted, isActive);
        return Ok(count);
    }

    [Authorize(Roles = RoleTypes.All)]
    [HttpPut]
    public async Task<IActionResult> UpdateUser([FromBody] UserUpdateDto userUpdateDto)
    {
        var updatedBy = User.Identity?.Name;
        var userRole = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;

        if (string.IsNullOrEmpty(userRole) && string.IsNullOrEmpty(updatedBy))
            return BadRequest("User is not logged in.");

        var result = await _userService.UpdateAsync(userUpdateDto, updatedBy!, userRole!);
        return Ok(result);
    }

    [Authorize(Roles = RoleTypes.Admins)]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteUser([FromRoute] int id)
    {
        var deletedBy = User.Identity?.Name;
        var userRole = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;

        if (string.IsNullOrEmpty(userRole) && string.IsNullOrEmpty(deletedBy))
            return BadRequest("User is not logged in.");

        var result = await _userService.DeleteAsync(id, deletedBy!, userRole!);
        return Ok(result);
    }

    [Authorize(Roles = RoleTypes.Admins)]
    [HttpGet("restore-user/{id:int}")]
    public async Task<IActionResult> RestoreUser([FromRoute] int id)
    {
        var restoredBy = User?.Identity?.Name;

        if (string.IsNullOrEmpty(restoredBy)) return BadRequest("User is not logged in.");

        var result = await _userService.RestoreDeletedAsync(id, restoredBy);
        return Ok(result);
    }

    [Authorize(Roles = RoleTypes.SuperAdmin)]
    [HttpDelete("hard-delete/{id:int}")]
    public async Task<IActionResult> HardDeleteUser([FromRoute] int id)
    {
        var result = await _userService.HardDeleteAsync(id);
        return Ok(result);
    }
}