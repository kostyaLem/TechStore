using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using TechStore.BL;
using TechStore.UI.Views.Administration;

namespace TechStore.UI;

public partial class App : Application
{
    private readonly IServiceProvider _serviceProvider;

    public App()
    {
        _serviceProvider = CreateServiceCollection();
    }

    private IServiceProvider CreateServiceCollection()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddBusinessLogicServices();
        serviceCollection.AddViewHelpers();

        serviceCollection.SetupViews();
        serviceCollection.SetupPages();

        return serviceCollection.BuildServiceProvider();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        _serviceProvider.GetRequiredService<CustomersView>().ShowDialog();
    }
}
