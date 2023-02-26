using Microsoft.Extensions.DependencyInjection;
using TechStore.BL.Auth;
using TechStore.BL.Services;
using TechStore.BL.Services.Interfaces;
using TechStore.DAL;

namespace TechStore.BL;

public static class IocConfiguration
{
    public static void AddBusinessLogicServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddRepositories();

        serviceCollection.AddTransient<IAuthorizationService, AuthorizationService>();
        serviceCollection.AddTransient<ICustomerService, CustomerService>();
    }
}
