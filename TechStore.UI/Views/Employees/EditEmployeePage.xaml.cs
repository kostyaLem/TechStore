using System.Windows.Controls;
using TechStore.BL.Models.Employees;
using TechStore.UI.ViewModels;

namespace TechStore.UI.Views.EditViews;

/// <summary>
/// Логика взаимодействия для EditEmployeePage.xaml
/// </summary>
public partial class EditEmployeePage : UserControl
{
    public EditEmployeePage(EditViewModel<Employee> context)
    {
        InitializeComponent();
        DataContext = context;
    }
}
