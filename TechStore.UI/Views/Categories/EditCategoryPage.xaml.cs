using System.Windows.Controls;
using TechStore.BL.Models.Categories;
using TechStore.UI.ViewModels;

namespace TechStore.UI.Views.Categories;

/// <summary>
/// Логика взаимодействия для EditCategoryPage.xaml
/// </summary>
public partial class EditCategoryPage : UserControl
{
    public EditCategoryPage(EditViewModel<Category> dataContext)
    {
        InitializeComponent();
        DataContext = dataContext;
    }
}
