using GlowAl.Application.Abstracts.Services;
using GlowAl.Infrastructure.Services;
using GlowAl.Persistence.Services;
using Microsoft.Extensions.DependencyInjection;



public static class ServiceRegistration
{
    public static void RegisterService(this IServiceCollection services) 
    {
        #region Repositories

        #endregion
        #region Servicies
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IAppUserService, AppUserService>();
        services.AddScoped<IEmailService,EmailService>();

        #endregion
    }
}
