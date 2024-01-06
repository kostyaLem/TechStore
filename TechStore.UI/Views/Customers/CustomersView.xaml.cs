using HandyControl.Controls;
using TechStore.UI.ViewModels.Customers;

namespace TechStore.UI.Views.Administration;

/// <summary>
/// Логика взаимодействия для CustomersView.xaml
/// </summary>
public partial class CustomersView : Window
{
    public CustomersView(CustomersViewModel customersViewModel)
    {
        InitializeComponent();
        DataContext = customersViewModel;
    }
}
