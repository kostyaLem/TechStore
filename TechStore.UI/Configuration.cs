using Microsoft.Extensions.DependencyInjection;
using TechStore.UI.Services;
using TechStore.UI.ViewModels;
using TechStore.UI.ViewModels.Administration;
using TechStore.UI.ViewModels.Employees;
using TechStore.UI.Views.Administration;
using TechStore.UI.Views.Employees;
using TechStore.UI.Views.Promos;

namespace TechStore.UI;

internal static class Configuration
{
    public static IServiceCollection AddViews(this IServiceCollection services)
    {
        services.AddTransient<AuthView>();
        services.AddTransient<AuthViewModel>();

        services.AddTransient<CustomersView>();
        services.AddTransient<CustomersViewModel>();

        services.AddTransient<EmployeesView>();
        services.AddTransient<EmployeesViewModel>();

        services.AddTransient<PromosView>();
        services.AddTransient<PromosViewModel>();

        return services;
    }

    public static IServiceCollection AddViewHelpers(this IServiceCollection services)
    {
        services.AddSingleton<IWindowDialogService, WindowDialogService>();

        return services;
    }
}