namespace TechStore.Domain.Models;

public class ProductStoredImage
{
    public int ProductId { get; set; }
    public int StoredImageId { get; set; }

    public Product Product { get; set; }
    public StoredImage StoredImage { get; set; }
}
