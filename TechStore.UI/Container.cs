using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using TechStore.BL;
using TechStore.BL.Models;
using TechStore.UI.Views.Administration;

namespace TechStore.UI;

public class Container
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

        //CreateResource("CommonStyleDictionary");
        //CreateResource("HandyControlStylesDictionary");
        //CreateResource("ImagesDictionary");

        serviceCollection.AddBusinessLogicServices();
        serviceCollection.AddViewHelpers();

        serviceCollection.AddViews();

        return serviceCollection.BuildServiceProvider();
    }

    public Window GetWindow(UserType userType)
        => userType switch
        {
            UserType.Admin or UserType.Employee =>
             _serviceProvider.GetRequiredService<AuthView>(),
            UserType.Customer =>
             _serviceProvider.GetRequiredService<AuthView>(),
            _ => throw new ArgumentException("Тип пользователя не поддерживается.")
        };

    public void CreateResource(string resourceName)
    {
        var resourceDictionary = new ResourceDictionary();
        resourceDictionary.Source = new Uri($"/TechStore.UI;component/Resources/Dictionaries/{resourceName}.xaml", UriKind.RelativeOrAbsolute);
        Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
    }
}
