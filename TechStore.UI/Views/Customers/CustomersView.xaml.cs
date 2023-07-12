using HandyControl.Controls;
using TechStore.UI.ViewModels.Administration;

namespace TechStore.UI.Views.Administration;

public partial class CustomersView : GlowWindow
{
    public CustomersView(CustomersViewModel customersViewModel)
    {
        InitializeComponent();
        DataContext = customersViewModel;
    }
}
