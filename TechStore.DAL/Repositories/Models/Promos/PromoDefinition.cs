namespace TechStore.DAL.Repositories.Models.Promos;

public class PromoDefinition
{
    public string Name { get; init; }
    public double Discount { get; init; }
    public bool IsActive { get; init; }
    public int CreatedByUserId { get; init; }
}
