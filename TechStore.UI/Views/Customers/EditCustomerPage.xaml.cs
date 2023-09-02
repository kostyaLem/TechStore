using System.Windows.Controls;
using TechStore.BL.Models.Customers;
using TechStore.UI.ViewModels;

namespace TechStore.UI.Views.EditViews;

/// <summary>
/// Логика взаимодействия для EditCustomerPage.xaml
/// </summary>
public partial class EditCustomerPage : UserControl
{
    public EditCustomerPage(EditViewModel<Customer> context)
    {
        InitializeComponent();
        DataContext = context;
    }
}
