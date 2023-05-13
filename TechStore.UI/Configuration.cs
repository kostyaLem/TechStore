using Microsoft.Extensions.DependencyInjection;
using TechStore.UI.Services;
using TechStore.UI.ViewModels.Administration;
using TechStore.UI.Views.Administration;

namespace TechStore.UI;

internal static class Configuration
{
    public static IServiceCollection AddViews(this IServiceCollection services)
    {
        services.AddTransient<AuthView, AuthView>();
        services.AddTransient<AuthViewModel>();

        services.AddTransient<CustomersView>();
        services.AddTransient<CustomersViewModel>();

        return services;
    }

    public static IServiceCollection AddViewHelpers(this IServiceCollection services)
    {
        services.AddSingleton<IWindowDialogService, WindowDialogService>();

        return services;
    }
}