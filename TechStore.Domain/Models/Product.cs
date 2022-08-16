namespace TechStore.Domain.Models;

public class Product
{
    public Product()
    {
        Images = new HashSet<StoredImage>();
        OrderProducts = new HashSet<OrderProduct>();
    }

    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }

    public virtual Category Category { get; set; }
    public virtual ICollection<StoredImage> Images { get; set; }
    public virtual ICollection<OrderProduct> OrderProducts { get; set; }
}
