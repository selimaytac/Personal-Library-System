using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using PLS.API.Helpers.Attributes;
using PLS.Entities.Concrete;
using PLS.Entities.Dtos;
using PLS.Entities.Enums;
using PLS.Services.Abstract;
using PLS.Shared.Results.ComplexTypes;

namespace PLS.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> Authenticate(AuthenticateRequest request)
        {
            var response = await _authService.AuthenticateAsync(request);

            if (response.ResultStatus == ResultStatus.Success)
                return Ok(Response);

            return Unauthorized(response);
        }
        
        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> Register(UserAddDto user)
        {
            var response = await _authService.RegisterAsync(user);
            return Ok(response);
        }

        [HttpGet("[action]/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var currentUser = HttpContext.Items["User"] as User;
            if (id != currentUser.Id && currentUser.Role.Name != RoleEnum.Admin.ToString())
            {
                return Unauthorized(new {message = "Unauthorized"});
            }

            var user = await _authService.GetAsync(id);
            return Ok(user);
        }
    }
}