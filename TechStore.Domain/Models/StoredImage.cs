namespace TechStore.Domain.Models;

public class StoredImage
{
    public StoredImage()
    {
        Products = new HashSet<Product>();
    }

    public int Id { get; set; }
    public string Title { get; set; }
    public byte[] Image { get; set; }

    public ICollection<Product> Products { get; set; }
}
