using System.Windows.Controls;
using TechStore.BL.Models.Products;
using TechStore.UI.ViewModels;

namespace TechStore.UI.Views.Products;

/// <summary>
/// Логика взаимодействия для EditProductPage.xaml
/// </summary>
public partial class EditProductPage : UserControl
{
    public EditProductPage(EditViewModel<Product> dataContext)
    {
        InitializeComponent();
        DataContext = dataContext;
    }
}
