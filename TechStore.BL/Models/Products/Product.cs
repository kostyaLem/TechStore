using DevExpress.Mvvm;

namespace TechStore.BL.Models.Products;

public class Product : BindableBase
{
    public string Name { get; init; }
    public double Price { get; init; }
}
