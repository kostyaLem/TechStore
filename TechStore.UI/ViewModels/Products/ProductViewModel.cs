using DevExpress.Mvvm;
using TechStore.BL.Models.Products;

namespace TechStore.UI.ViewModels.Products;

public class ProductViewModel : BindableBase
{
    public Product Product { get; init; } = new Product() { Name = "IPhone 12S", Price = 12500 };
}
