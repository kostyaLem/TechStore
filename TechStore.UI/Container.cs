using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using TechStore.BL;
using TechStore.BL.Models;
using TechStore.UI.Models;
using TechStore.UI.Services;
using TechStore.UI.ViewModels.Categories;
using TechStore.UI.ViewModels.Customers;
using TechStore.UI.ViewModels.Employees;
using TechStore.UI.ViewModels.Products;
using TechStore.UI.ViewModels.Promos;
using TechStore.UI.Views;
using TechStore.UI.Views.Administration;
using TechStore.UI.Views.Categories;
using TechStore.UI.Views.Employees;
using TechStore.UI.Views.Products;
using TechStore.UI.Views.Promos;

namespace TechStore.UI;

public static class Container
{
    public static IServiceProvider ServiceProvider { get; }

    // Текущий авторизированный пользователя в системе
    public static CurrentUser CurrentUser { get; set; }
    public static bool IsAdmin => true; //CurrentUser.UserType == UserType.Admin;

    static Container()
    {
        HandyControl.Themes.ThemeManager.Current.ApplicationTheme = HandyControl.Themes.ApplicationTheme.Dark;

        ServiceProvider = CreateServiceCollection();
    }

    private static IServiceProvider CreateServiceCollection()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection
            .AddBusinessLogicServices()
            .AddViewHelpers()
            .AddViewItems()
            .AddViews();

        return serviceCollection.BuildServiceProvider();
    }

    public static Window GetWindow(UserType userType)
        => userType switch
        {
            UserType.Admin or UserType.Employee =>
             ServiceProvider.GetRequiredService<PromosView>(),
            UserType.Customer =>
             ServiceProvider.GetRequiredService<MainView>(),
            _ => throw new ArgumentException("Тип пользователя не поддерживается.")
        };

    private static IServiceCollection AddViewItems(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton(new ViewItem
        {
            Title = ViewTitleService.Get<CategoriesViewModel>(),
            ImageName = "CategoriesImage",
            Description = "Категории товаров",
            ViewType = typeof(CategoriesView)
        });
        serviceCollection.AddSingleton(new ViewItem
        {
            Title = ViewTitleService.Get<CustomersViewModel>(),
            ImageName = "CustomersImage",
            Description = "Учет клиентов",
            ViewType = typeof(CustomersView)
        });
        serviceCollection.AddSingleton(new ViewItem
        {
            Title = ViewTitleService.Get<EmployeesViewModel>(),
            ImageName = "EmployeesImage",
            Description = "Учет работников",
            ViewType = typeof(EmployeesView)
        });
        serviceCollection.AddSingleton(new ViewItem
        {
            Title = ViewTitleService.Get<ProductViewModel>(),
            ImageName = "ProductsImage",
            Description = "Склад",
            ViewType = typeof(ProductsView)
        });
        serviceCollection.AddSingleton(new ViewItem
        {
            Title = ViewTitleService.Get<PromosViewModel>(),
            ImageName = "PromosImage",
            Description = "Промокоды на товары",
            ViewType = typeof(PromosView)
        });

        return serviceCollection;
    }
}
