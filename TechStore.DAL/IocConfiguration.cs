using Microsoft.Extensions.DependencyInjection;
using TechStore.DAL.Context;
using TechStore.DAL.Repositories;
using TechStore.DAL.Repositories.Interfaces;

namespace TechStore.DAL;

public static class IocConfiguration
{
    public static void AddRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<TechStoreContextFactory>();

        serviceCollection.AddTransient<IAuthorizationRepository, AuthorizationRepository>();
        serviceCollection.AddTransient<ICustomerRepository, CustomerRepository>();
        serviceCollection.AddTransient<IEmployeeRepository, EmployeeRepository>();
        serviceCollection.AddTransient<IPromoRepository, PromoRepository>();
        serviceCollection.AddTransient<ICategoryRepository, CategoryRepository>();
        serviceCollection.AddTransient<IProductRepository, ProductRepository>();
        serviceCollection.AddTransient<IStatisticRepository, StatisticRepository>();
        serviceCollection.AddTransient<IOrderRepository, OrderRepository>();
    }
}
