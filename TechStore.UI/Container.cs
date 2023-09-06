using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using TechStore.BL;
using TechStore.BL.Models;
using TechStore.UI.Views;
using TechStore.UI.Views.Products;

namespace TechStore.UI;

public sealed class Container
{
    private readonly IServiceProvider _serviceProvider;

    // Текущий авторизированный пользователя в системе
    public static CurrentUser CurrentUser { get; set; }
    public static bool IsAdmin => true; //CurrentUser.UserType == UserType.Admin;

    public Container()
    {
        _serviceProvider = CreateServiceCollection();
    }

    private IServiceProvider CreateServiceCollection()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection
            .AddBusinessLogicServices()
            .AddViewHelpers()
            .AddViews();

        return serviceCollection.BuildServiceProvider();
    }

    public Window GetWindow(UserType userType)
        => userType switch
        {
            UserType.Admin or UserType.Employee =>
             _serviceProvider.GetRequiredService<ProductsView>(),
            UserType.Customer =>
             _serviceProvider.GetRequiredService<MainView>(),
            _ => throw new ArgumentException("Тип пользователя не поддерживается.")
        };
}
