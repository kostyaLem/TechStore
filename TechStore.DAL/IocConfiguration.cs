using Microsoft.Extensions.DependencyInjection;
using TechStore.DAL.Context;
using TechStore.DAL.Repositories;

namespace TechStore.DAL;

public static class IocConfiguration
{
    public static void AddRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<TechStoreContextFactory>();
        serviceCollection.AddTransient<TechStoreContext>(x =>
        {
            var factory = x.GetRequiredService<TechStoreContextFactory>();
            return factory.CreateDbContext();
        });

        serviceCollection.AddTransient<AuthorizationRepository>();
    }
}
