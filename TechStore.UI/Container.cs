using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using TechStore.BL;
using TechStore.BL.Models;
using TechStore.UI.Views.Employees;
using TechStore.UI.Views.Promos;

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
             _serviceProvider.GetRequiredService<PromosView>(),
            UserType.Customer =>
             _serviceProvider.GetRequiredService<EmployeesView>(),
            _ => throw new ArgumentException("Тип пользователя не поддерживается.")
        };
}
