using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PLS.Entities.ConstTypes;
using PLS.Entities.Dtos;
using PLS.Services.Abstract;
using PLS.Services.Utilities.Abstract;
using PLS.Shared.Results.ComplexTypes;

namespace PLS.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    public async Task<IActionResult> Authenticate(AuthenticateRequest request)
    {
        var response = await _authService.AuthenticateAsync(request);

        if (response.ResultStatus == ResultStatus.Success)
            return Ok(response.Data);

        return Unauthorized(response);
    }

    [HttpPost]
    public async Task<IActionResult> Register(UserAddDto user)
    {
        var response = await _authService.RegisterAsync(user);
        return Ok(response);
    }

    [Authorize(Roles = RoleTypes.Admin)]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _authService.GetAsync(id);
        return Ok(user);
    }
}