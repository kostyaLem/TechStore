namespace TechStore.Domain.Models;

public class Customer
{
    public Customer()
    {
        Orders = new HashSet<Order>();
    }

    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Birthday { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public bool IsActive { get; set; }
    public int UserId { get; set; }

    public virtual User User { get; set; }
    public virtual ICollection<Order> Orders { get; set; }

}
