using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;
using PLS.Entities.Concrete;
using PLS.Entities.Dtos;
using PLS.Entities.Enums;
using PLS.Services.Abstract;
using PLS.Shared.Results.ComplexTypes;

namespace PLS.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        
        [HttpPost("[action]")]
        public async Task<IActionResult> Authenticate(AuthenticateRequest request)
        {
            var response = await _authService.AuthenticateAsync(request);

            if (response.ResultStatus == ResultStatus.Success)
                return Ok(response.Data);

            return Unauthorized(response);
        }
        
        [HttpPost("[action]")]
        public async Task<IActionResult> Register(UserAddDto user)
        {
            var response = await _authService.RegisterAsync(user);
            return Ok(response);
        }

        [Authorize(Roles = nameof(RoleEnum.Admin))]
        [HttpGet("[action]/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _authService.GetAsync(id);
            return Ok(user);
        }
    }
}