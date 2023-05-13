using System.Windows;

namespace TechStore.UI.Client;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
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
