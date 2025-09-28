using GlowAl.Application.Abstracts.Repositories;
using GlowAl.Application.Abstracts.Services;
using GlowAl.Infrastructure.Services;
using GlowAl.Persistence.Repositories;
using GlowAl.Persistence.Services;
using Microsoft.Extensions.DependencyInjection;



public static class ServiceRegistration
{
    public static void RegisterService(this IServiceCollection services) 
    {
        #region Repositories
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICareProductRepository, CareProductRepository>();
        #endregion
        #region Servicies
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IAppUserService, AppUserService>();
        services.AddScoped<IEmailService,EmailService>();
        services.AddScoped<ICareProductService, CareProductService>();

        #endregion
    }
}
