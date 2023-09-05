namespace TechStore.Domain.Models;

public class Employee
{
    public Employee()
    {
        Orders = new HashSet<Order>();
        PromoCodes = new HashSet<PromoCode>();
    }

    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Birthday { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public int UserId { get; set; }
    public byte[]? Image { get; set; }

    public virtual User User { get; set; }
    public virtual ICollection<Order> Orders { get; set; }
    public virtual ICollection<PromoCode> PromoCodes { get; set; }
}
