namespace TechStore.BL.Models.Promos;

public sealed class Promo
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Discount { get; set; }
    public DateTime CreatedOn { get; set; }
    public bool IsActive { get; set; }
    public string CreatedBy { get; set; }
}
