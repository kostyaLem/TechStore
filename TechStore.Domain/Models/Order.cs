namespace TechStore.Domain.Models;

public class Order
{
    public Order()
    {
        OrderProducts = new HashSet<OrderProduct>();
    }

    public int Id { get; set; }
    public int CustomerId { get; set; }
    public int EmployeeId { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }
    public DateTime? ClosedOn { get; set; }

    public virtual Customer Customer { get; set; }
    public virtual Employee Employee { get; set; }
    public virtual ICollection<OrderProduct> OrderProducts { get; set; }
}
