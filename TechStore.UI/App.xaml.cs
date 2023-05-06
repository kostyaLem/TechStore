using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using TechStore.BL;
using TechStore.BL.Models;
using TechStore.UI.ViewModels.Administration;
using TechStore.UI.Views.Administration;

namespace TechStore.UI;

public partial class App : Application
{
    private readonly IServiceProvider _serviceProvider;

    // Текущий авторизированный пользователя в системе
    public static CurrentUser CurrentUser { get; set; }
    public static bool IsAdmin => true; //CurrentUser.UserType == UserType.Admin;

    public App()
    {
        _serviceProvider = CreateServiceCollection();
    }

    private IServiceProvider CreateServiceCollection()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddBusinessLogicServices();
        serviceCollection.AddViewHelpers();

        serviceCollection.AddViews();

        return serviceCollection.BuildServiceProvider();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        _serviceProvider.GetRequiredService<CustomersView>().ShowDialog();
    }
}

internal static class Configuration
{
    public static ServiceCollection AddViews(this ServiceCollection services)
    {
        services.AddTransient<CustomersView>();
        services.AddTransient<CustomersViewModel>();

        return services;
    }
}
