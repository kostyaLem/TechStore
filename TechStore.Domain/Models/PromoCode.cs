namespace TechStore.Domain.Models;

public class PromoCode
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Discount { get; set; }
    public DateTime CreatedOn { get; set; }

    public bool Active { get; set; }
    public int CreatedByEmployeeId { get; set; }
    public virtual Employee Employee { get; set; }
}


