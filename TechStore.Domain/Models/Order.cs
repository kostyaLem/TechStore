namespace TechStore.Domain.Models;

public class Order
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public int CustomerId { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }
    public DateTime? ClosedOn { get; set; }

    public Customer Customer { get; set; }
    public Employee Employee { get; set; }
}
