using System.Windows.Controls;
using TechStore.UI.ViewModels.Products;

namespace TechStore.UI.Views.Products;

/// <summary>
/// Логика взаимодействия для Product.xaml
/// </summary>
public partial class ProductCard : UserControl
{
    public ProductCard(ProductViewModel dataContext)
    {
        InitializeComponent();
        DataContext = dataContext;
    }
}
