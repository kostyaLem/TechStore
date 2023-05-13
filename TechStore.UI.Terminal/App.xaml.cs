using System.Windows;

namespace TechStore.UI.Terminal;

public partial class App : Application
{
    private readonly UI.Container _container;

    public App()
    {
        _container = new UI.Container();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        _container.GetWindow(BL.Models.UserType.Admin).ShowDialog();
    }
}
