using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PLS.Entities.Configures;
using PLS.Services.Abstract;
using PLS.Services.Concrete;
using PLS.Services.Utilities.Abstract;

namespace PLS.API.Helpers.Middlewares;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly AppSettings _appSettings;

    public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
    {
        _next = next;
        _appSettings = appSettings.Value;
    }

    public async Task Invoke(HttpContext context, IAuthService authService, IJwtUtils jwtUtils)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        var userId = jwtUtils.ValidateJwtToken(token);

        if (userId != null)
        {
            var user = await authService.GetAsync(userId.Value);
            context.Items["User"] = user.Data.User;
        }

        await _next(context);
    }

}