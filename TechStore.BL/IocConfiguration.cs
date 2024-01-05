using Microsoft.Extensions.DependencyInjection;
using TechStore.BL.Auth;
using TechStore.BL.Services;
using TechStore.BL.Services.Interfaces;
using TechStore.DAL;

namespace TechStore.BL;

public static class IocConfiguration
{
    public static IServiceCollection AddBusinessLogicServices(this IServiceCollection services)
    {
        services.AddRepositories();

        services.AddTransient<IAuthorizationService, AuthorizationService>();
        services.AddTransient<ICustomerService, CustomerService>();
        services.AddTransient<IEmployeeService, EmployeeService>();
        services.AddTransient<IPromoService, PromoService>();
        services.AddTransient<ICategoryService, CategoryService>();
        services.AddTransient<IProductService, ProductService>();
        services.AddTransient<IStatisticService, StatisticService>();

        return services;
    }
}
