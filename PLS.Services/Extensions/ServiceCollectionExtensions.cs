using Microsoft.Extensions.DependencyInjection;
using PLS.Data.Abstract;
using PLS.Data.Concrete;
using PLS.Data.Concrete.EntityFramework.Contexts;
using PLS.Services.Abstract;
using PLS.Services.Concrete;
using PLS.Services.Utilities.Abstract;
using PLS.Services.Utilities.Concrete;

namespace PLS.Services.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection LoadMyServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
        
        serviceCollection.AddScoped<IJwtUtils, JwtUtils>();
        serviceCollection.AddScoped<IAuthUtils, AuthUtils>();
        
        serviceCollection.AddScoped<IAuthService, AuthService>();
        serviceCollection.AddScoped<IUserService, UserService>();
        serviceCollection.AddScoped<ISourceService, SourceService>();
        serviceCollection.AddScoped<ITagService, TagService>();
        
        return serviceCollection;
    }
}