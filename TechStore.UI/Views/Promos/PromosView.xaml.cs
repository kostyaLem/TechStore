using HandyControl.Controls;
using TechStore.UI.ViewModels.Promos;

namespace TechStore.UI.Views.Promos;

/// <summary>
/// Логика взаимодействия для PromosView.xaml
/// </summary>
public partial class PromosView : Window
{
    public PromosView(PromosViewModel dataContext)
    {
        InitializeComponent();
        DataContext = dataContext;
    }
}
