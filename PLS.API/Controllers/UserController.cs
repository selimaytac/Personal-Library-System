using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PLS.Entities.ConstTypes;
using PLS.Entities.Dtos;
using PLS.Services.Abstract;
using PLS.Shared.Results.Abstract;
using PLS.Shared.Results.ComplexTypes;
using PLS.Shared.Results.Concrete;

namespace PLS.API.Controllers;

[ApiController]
[Route("api/users")]
[Authorize(Roles = RoleTypes.SuperAdmin + "," + RoleTypes.Admin)]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetUser([FromRoute] int id)
    {
        var user = await _userService.GetAsync(id);
        return Ok(user);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllUsers(
        [FromQuery(Name = "nonDeleted")] bool nonDeleted = false,
        [FromQuery(Name = "onlyActive")] bool onlyActive = false)
    {
        // Short Description:
        // When nonDeleted is true, only non-deleted users are returned.
        // When onlyActive is true, nondeleted and active users are returned,
        // You can't get active and deleted users together, it returns all users.
        IDataResult<UserListDto> users; 
        
        if (!(nonDeleted == false && onlyActive == false))
        {
            users = nonDeleted switch
            {
                true when onlyActive == false => await _userService.GetAllByNonDeletedAsync(),
                true when onlyActive == true => await _userService.GetAllByNonDeletedAndActiveAsync(),
                _ => await _userService.GetAllAsync()
            };
        }
        else
        {
            users = await _userService.GetAllAsync();
        }

        return Ok(users);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUser([FromBody] UserUpdateDto userUpdateDto)
    {
        var updatedBy = User?.Identity?.Name;
        
        if(string.IsNullOrEmpty(updatedBy)) return BadRequest("User is not logged in.");
        
        var result = await _userService.UpdateAsync(userUpdateDto, updatedBy);
        return Ok(result);
    }
    
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteUser([FromRoute] int id)
    {
        var deletedBy = User?.Identity?.Name;
        
        if(string.IsNullOrEmpty(deletedBy)) return BadRequest("User is not logged in.");
        
        var result = await _userService.DeleteAsync(id, deletedBy);
        return Ok(result);
    }
    
    [HttpPut("{id:int}")]
    public async Task<IActionResult> RestoreUser([FromRoute] int id)
    {
        var restoredBy = User?.Identity?.Name;
        
        if(string.IsNullOrEmpty(restoredBy)) return BadRequest("User is not logged in.");
        
        var result = await _userService.DeleteAsync(id, restoredBy);
        return Ok(result);
    }
    
    [HttpDelete("harddelete/{id:int}")]
    public async Task<IActionResult> HardDeleteUser([FromRoute] int id)
    {
        var result = await _userService.HardDeleteAsync(id);
        return Ok(result);
    }
    
}