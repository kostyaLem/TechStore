using System.Windows.Controls;
using TechStore.BL.Models.Products;

namespace TechStore.UI.Views.Products;

/// <summary>
/// Логика взаимодействия для Product.xaml
/// </summary>
public partial class ProductCard : UserControl
{
    public ProductCard(Product dataContext)
    {
        InitializeComponent();
        DataContext = dataContext;
    }

    public ProductCard()
    {
        InitializeComponent();
    }
}
