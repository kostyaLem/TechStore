using System.Windows.Controls;
using TechStore.BL.Models.Customers;
using TechStore.UI.ViewModels;

namespace TechStore.UI.Views.EditViews;
/// <summary>
/// Логика взаимодействия для EditCustomerView.xaml
/// </summary>
public partial class EditEmployeePage : UserControl
{
    public EditEmployeePage(EditViewModel<Customer> context)
    {
        InitializeComponent();
        DataContext = context;
    }
}
