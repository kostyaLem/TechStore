using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using TechStore.UI.ViewModels;
using TechStore.UI.ViewModels.PageViewModels;

namespace TechStore.UI;

internal static class IocExtensions
{
    public static void SetupViews(this IServiceCollection services)
    {
        var viewModels = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(x => IsViewModel(x) || IsView(x))
            .ToList();

        viewModels.ForEach(vm => services.AddSingleton(vm));
    }

    public static void SetupPages(this IServiceCollection services)
    {
        var viewModels = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(type => IsPageViewModel(type) || IsPage(type))
            .ToList();

        viewModels.ForEach(vm => services.AddTransient(vm));
    }

    private static bool IsViewModel(Type objectType)
        => objectType.IsSubclassOf(typeof(BaseViewModel));

    private static bool IsView(Type objectType)
        => objectType.IsSubclassOf(typeof(Window));

    private static bool IsPageViewModel(Type objectType)
        => objectType.IsSubclassOf(typeof(BasePageViewModel));

    private static bool IsPage(Type objectType)
        => objectType.IsSubclassOf(typeof(Page));
}
