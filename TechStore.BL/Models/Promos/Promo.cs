using DevExpress.Mvvm;
using TechStore.BL.Models.Categories;

namespace TechStore.BL.Models.Promos;

public sealed class Promo : BindableBase
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Discount { get; set; }
    public DateTime CreatedOn { get; set; }
    public string CreatedBy { get; set; }
    public IReadOnlyList<Category> Categories { get; set; }
    public bool IsActive
    {
        get => GetValue<bool>(nameof(IsActive));
        set => SetValue(value, nameof(IsActive));
    }

    public Promo()
    {
        Discount = 5;
    }
}
