using Microsoft.Extensions.DependencyInjection;
using TechStore.BL.Auth;
using TechStore.DAL;

namespace TechStore.BL;

public static class IocConfiguration
{
    public static void AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddRepositories();

        serviceCollection.AddTransient<AuthorizationService>();
    }
}
