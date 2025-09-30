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
        services.AddScoped<IReviewRepository, ReviewRepository>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IFavoriteRepository, FavoriteRepository>();
        services.AddScoped<ISkinTypeRepository, SkinTypeRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderItemRepository, OrderItemRepository>();
        #endregion

        #region Services
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IAppUserService, AppUserService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<ICareProductService, CareProductService>();
        services.AddScoped<IReviewService, ReviewService>();
        services.AddScoped<IFavoriteService, FavoriteService>();
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<ISkinTypeService, SkinTypeService>();
        services.AddScoped<IOrderService, OrderService>();
       

        services.AddScoped<IAIQueryHistoryService, AIQueryHistoryService>();
        services.AddHttpClient<IAIService, AIService>();

        #endregion
    }
}
