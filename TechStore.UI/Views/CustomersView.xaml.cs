using HandyControl.Controls;
using TechStore.UI.ViewModels.Administration;

namespace TechStore.UI.Views.Administration
{
    /// <summary>
    /// Interaction logic for CustomersView.xaml
    /// </summary>
    public partial class CustomersView : GlowWindow
    {
        public CustomersView(CustomersViewModel customersViewModel)
        {
            InitializeComponent();
            DataContext = customersViewModel;
        }
    }
}
