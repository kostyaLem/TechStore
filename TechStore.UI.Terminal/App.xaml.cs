using System.Windows;

namespace TechStore.UI.Terminal;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        Container.CurrentUser = new(3, "qwe", BL.Models.UserType.Admin);
        Container.GetWindow(BL.Models.UserType.Customer).ShowDialog();
    }
}
