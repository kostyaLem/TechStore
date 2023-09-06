using DevExpress.Mvvm;

namespace TechStore.BL.Models.Categories;

public sealed class Category : BindableBase
{
    public int Id { get; init; }
    public string Name { get; init; }
    public IReadOnlyList<string> Products { get; init; }

    public override string ToString() => Name;
}
