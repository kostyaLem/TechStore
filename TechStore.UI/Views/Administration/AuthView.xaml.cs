using HandyControl.Controls;
using TechStore.UI.ViewModels.Administration;

namespace TechStore.UI.Views.Administration;

/// <summary>
/// Логика взаимодействия для AuthView.xaml
/// </summary>
public partial class AuthView : Window
{
    public AuthView(AuthViewModel authViewModel)
    {
        InitializeComponent();
        DataContext = authViewModel;
    }
}
