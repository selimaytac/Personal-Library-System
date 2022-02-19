using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PLS.Entities.Concrete;
using PLS.Entities.ConstTypes;
using PLS.Entities.Dtos;
using PLS.Services.Abstract;

namespace PLS.API.Controllers;

[Route("api/sources")]
[ApiController]
public class SourceController : ControllerBase
{
    private readonly ISourceService _sourceService;

    public SourceController(ISourceService sourceService)
    {
        _sourceService = sourceService;
    }

    [Authorize(Roles = RoleTypes.All)]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var source = await _sourceService.GetAsync(id);
        return Ok(source);
    }

    [Authorize(Roles = RoleTypes.All)]
    [HttpGet]
    public async Task<IActionResult> GetAllSources(
        [FromQuery(Name = "isDeleted")] bool isDeleted = false,
        [FromQuery(Name = "isActive")] bool isActive = true)
    {
        var users = await _sourceService.GetAllAsync(isDeleted, isActive);

        return Ok(users);
    }

    [Authorize(Roles = RoleTypes.All)]
    [HttpGet("get-all-by-category/{categoryId:int}")]
    public async Task<IActionResult> GetAllSourcesByCategory(
        [FromRoute] int categoryId,
        [FromQuery] bool isDeleted = false,
        [FromQuery] bool isActive = true)
    {
        var sources = await _sourceService.GetAllByCategoryAsync(categoryId, isDeleted, isActive);

        return Ok(sources);
    }

    [Authorize(Roles = RoleTypes.All)]
    [HttpPost("get-all-by-tags")]
    public async Task<IActionResult> GetAllSourcesByTags(
        [FromBody] int[] tagIds,
        [FromQuery] bool isDeleted = false,
        [FromQuery] bool isActive = true)
    {
        var sources = await _sourceService.GetAllByTagsAsync(tagIds, isDeleted, isActive);

        return Ok(sources);
    }

    [Authorize(Roles = RoleTypes.All)]
    [HttpGet("")]
    public async Task<IActionResult> GetSourceCount(
        [FromQuery(Name = "isDeleted")] bool isDeleted = false,
        [FromQuery(Name = "isActive")] bool isActive = true)
    {
        var count = await _sourceService.GetSourceCount(isDeleted, isActive);

        return Ok(count);
    }

    [Authorize(Roles = RoleTypes.All)]
    [HttpPost]
    public async Task<IActionResult> AddSource(SourceAddDto source)
    {
        var createdByUser = User.Identity?.Name;

        if (string.IsNullOrEmpty(createdByUser)) return BadRequest("User is not logged in.");

        var response = await _sourceService.AddAsync(source, createdByUser);

        return Ok(response);
    }

    [Authorize(Roles = RoleTypes.All)]
    [HttpPut]
    public async Task<IActionResult> UpdateSource(SourceUpdateDto sourceUpdateDto)
    {
        var userName = User.Identity?.Name;
        var userRole = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;

        if (string.IsNullOrEmpty(userRole) && string.IsNullOrEmpty(userName))
            return BadRequest("User is not logged in.");

        var response = await _sourceService.UpdateAsync(sourceUpdateDto, userName!, userRole!);

        return Ok(response);
    }
}