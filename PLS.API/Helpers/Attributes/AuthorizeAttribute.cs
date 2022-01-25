using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PLS.Entities.Concrete;
using PLS.Entities.Enums;

namespace PLS.API.Helpers.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    private readonly IList<RoleEnum> _roles;

    public AuthorizeAttribute(params RoleEnum[] roles)
    {
        _roles = roles ?? new RoleEnum[] { };
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
        if (allowAnonymous)
            return;
        var user2 = context.HttpContext.Items["User"];
        var user = context.HttpContext.Items["User"] as User;
        if (user == null || (_roles.Any() && !_roles.Select(x => x.ToString()).Contains(user.Role.Name)))
        {
            context.Result = new JsonResult(new {message = "Unauthorized"})
                {StatusCode = StatusCodes.Status401Unauthorized};
        }
        
    }
}