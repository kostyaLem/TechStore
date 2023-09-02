using HandyControl.Controls;
using TechStore.UI.ViewModels.Categories;

namespace TechStore.UI.Views.Categories;

/// <summary>
/// Логика взаимодействия для CategoriesView.xaml
/// </summary>
public partial class CategoriesView : GlowWindow
{
    public CategoriesView(CategoriesViewModel dataContext)
    {
        InitializeComponent();
        DataContext = dataContext;
    }
}
