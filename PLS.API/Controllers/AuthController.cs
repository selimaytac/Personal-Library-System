using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PLS.Entities.ConstTypes;
using PLS.Entities.Dtos;
using PLS.Services.Abstract;
using PLS.Services.Utilities.Abstract;
using PLS.Shared.Results.ComplexTypes;

namespace PLS.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
[Authorize(Roles = RoleTypes.SuperAdmin + "," + RoleTypes.Admin)]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Authenticate(AuthenticateRequest request)
    {
        var response = await _authService.AuthenticateAsync(request);

        if (response.ResultStatus == ResultStatus.Success)
            return Ok(response.Data);

        return Unauthorized(response);
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Register(UserAddDto user)
    {
        var response = await _authService.RegisterAsync(user);
        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _authService.GetAsync(id);
        return Ok(user);
    }
}