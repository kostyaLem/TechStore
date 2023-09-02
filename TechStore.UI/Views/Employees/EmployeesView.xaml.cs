using HandyControl.Controls;
using TechStore.UI.ViewModels.Employees;

namespace TechStore.UI.Views.Employees;

/// <summary>
/// Логика взаимодействия для EmployeesView.xaml
/// </summary>
public partial class EmployeesView : GlowWindow
{
    public EmployeesView(EmployeesViewModel employeeViewModel)
    {
        InitializeComponent();
        DataContext = employeeViewModel;
    }
}
