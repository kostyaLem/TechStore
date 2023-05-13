using Microsoft.Extensions.DependencyInjection;
using TechStore.UI.ViewModels.Administration;
using TechStore.UI.Views.Administration;

namespace TechStore.UI;

internal static class Configuration
{
    public static ServiceCollection AddViews(this ServiceCollection services)
    {
        services.AddTransient<AuthView, AuthView>();
        services.AddTransient<AuthViewModel>();

        services.AddTransient<CustomersView>();
        services.AddTransient<CustomersViewModel>();

        return services;
    }
}