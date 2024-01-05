namespace TechStore.BL.Models.Promos;

public sealed class CreatePromoRequest
{
    public string Name { get; init; }
    public double Discount { get; init; }
    public IReadOnlyList<int> CategoriesIds { get; init; }
    public int CreatedByUserId { get; init; }
}