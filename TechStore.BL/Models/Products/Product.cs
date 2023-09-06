using DevExpress.Mvvm;
using TechStore.BL.Models.Categories;

namespace TechStore.BL.Models.Products;

public class Product : BindableBase
{
    public int Id { get; init; }
    public string Title{ get; init; }
    public string Description { get; init; }
    public decimal Price { get; init; }
    public int SalesCount { get; init; }
    public Category Category { get; init; }
    public DateTime CreatedOn { get; init; }
    public byte[]? Image { get; init; }
    public byte[]? SmallImage { get; init; }
    public bool IsActive
    {
        get => GetValue<bool>(nameof(IsActive));
        set => SetValue(value, nameof(IsActive));
    }
}
