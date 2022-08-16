using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using TechStore.UI.Views;

namespace TechStore.UI;

public partial class App : Application
{
    private IServiceProvider _serviceProvider;

    public App()
    {
        var serviceCollection = CreateServiceCollection();

        _serviceProvider = serviceCollection.BuildServiceProvider();
        _serviceProvider.GetRequiredService<MainView>().ShowDialog();
    }

    private ServiceCollection CreateServiceCollection()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.SetupViews();
        serviceCollection.SetupPages();

        return serviceCollection;
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
    }
}
