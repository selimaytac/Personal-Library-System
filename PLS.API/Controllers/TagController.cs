using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Construction;
using PLS.Entities.ConstTypes;
using PLS.Entities.Dtos;
using PLS.Services.Abstract;

namespace PLS.API.Controllers;

[Route("api/tags")]
[Authorize(Roles = RoleTypes.All)]
[ApiController]
public class TagController : Controller
{
    private readonly ITagService _tagService;

    public TagController(ITagService tagService)
    {
        _tagService = tagService;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetTag([FromRoute] int id)
    {
        var tag = await _tagService.GetAsync(id);
        return Ok(tag);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTags(
        [FromQuery(Name = "isDeleted")] bool isDeleted = false,
        [FromQuery(Name = "isActive")] bool isActive = true)
    {
        var tags = await _tagService.GetAllAsync(isDeleted, isActive);

        return Ok(tags);
    }

    [HttpGet("get-tag-count")]
    public async Task<IActionResult> GetTagCount(
        [FromQuery(Name = "isDeleted")] bool isDeleted = false,
        [FromQuery(Name = "isActive")] bool isActive = true)
    {
        var tagCount = await _tagService.GetTagCount(isDeleted, isActive);

        return Ok(tagCount);
    }

    [HttpPost]
    public async Task<IActionResult> AddTag(TagAddDto tagAddDto)
    {
        var createdByUser = User.Identity?.Name;

        if (string.IsNullOrEmpty(createdByUser)) return BadRequest("User is not logged in");

        var response = await _tagService.AddAsync(tagAddDto, createdByUser);

        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateTag(TagUpdateDto tagUpdateDto)
    {
        var updatedByUser = User.Identity?.Name;
        var userRole = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;

        if (string.IsNullOrEmpty(updatedByUser) && string.IsNullOrEmpty(userRole))
            return BadRequest("User is not logged in");

        var response = await _tagService.UpdateAsync(tagUpdateDto, updatedByUser!, userRole!);

        return Ok(response);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteTag([FromRoute] int id)
    {
        var deletedByUser = User.Identity?.Name;
        var userRole = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;

        if (string.IsNullOrEmpty(deletedByUser) && string.IsNullOrEmpty(userRole))
            return BadRequest("User is not logged in");

        var response = await _tagService.DeleteAsync(id, deletedByUser!, userRole!);

        return Ok(response);
    }

    [HttpGet("restore/{id:int}")]
    public async Task<IActionResult> RestoreTag([FromRoute] int id)
    {
        var restoredByUser = User.Identity?.Name;
        var userRole = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;

        if (string.IsNullOrEmpty(restoredByUser) && string.IsNullOrEmpty(userRole))
            return BadRequest("User is not logged in");

        var response = await _tagService.RestoreDeletedAsync(id, restoredByUser!, userRole!);

        return Ok(response);
    }

    [Authorize(Roles = RoleTypes.Admins)]
    [HttpDelete("hard-delete/{id:int}")]
    public async Task<IActionResult> HardDeleteTag([FromRoute] int id)
    {
        var response = await _tagService.HardDeleteAsync(id);
        return Ok(response);
    }
}