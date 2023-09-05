namespace TechStore.Domain.Models;

public class Product
{
    public Product()
    {
        OrderProducts = new HashSet<OrderProduct>();
    }

    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime CreatedOn { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int? CategoryId { get; set; }
    public byte[]? Image { get; set; }
    public bool IsActive { get; set; }

    public virtual Category Category { get; set; }
    public virtual ICollection<OrderProduct> OrderProducts { get; set; }
}
