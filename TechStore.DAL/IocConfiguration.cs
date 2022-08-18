using Microsoft.Extensions.DependencyInjection;
using TechStore.DAL.Context;
using TechStore.DAL.Repositories;

namespace TechStore.DAL;

public static class IocConfiguration
{
    public static void AddRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddDbContextFactory<TechStoreContext, TechStoreContextFactory>();

        serviceCollection.AddTransient<AuthorizationRepository>();
    }
}
