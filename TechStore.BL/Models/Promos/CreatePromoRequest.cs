namespace TechStore.BL.Models.Promos;

public sealed class CreatePromoRequest
{
    public string Name { get; init; }
    public double Discount { get; init; }
    public int CreatedByEmployeeId { get; init; }
}