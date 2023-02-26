using Microsoft.Extensions.DependencyInjection;
using TechStore.UI.Services;
using TechStore.UI.Services.Interfaces;

namespace TechStore.UI;
internal static class IocConfiguration
{
    public static void AddViewHelpers(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IDialogView, DialogView>();

        serviceCollection.AddTransient<ICustomerDialogView, CustomerDialogView>();
    }
}
