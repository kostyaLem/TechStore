using HandyControl.Controls;
using System.Windows.Controls;
using TechStore.UI.ViewModels.Products;

namespace TechStore.UI.Views.Products;

/// <summary>
/// Логика взаимодействия для Product.xaml
/// </summary>
public partial class Product : UserControl
{
    public Product(ProductViewModel dataContext)
    {
        InitializeComponent();
        DataContext = dataContext;
    }

    public Product()
    {
        InitializeComponent();
        DataContext = new ProductViewModel();
    }
}
