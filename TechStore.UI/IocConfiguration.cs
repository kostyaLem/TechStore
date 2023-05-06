using Microsoft.Extensions.DependencyInjection;
using TechStore.UI.Services;

namespace TechStore.UI;
internal static class IocConfiguration
{
    public static void AddViewHelpers(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IWindowDialogService, WindowDialogService>();
    }
}
