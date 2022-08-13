namespace TechStore.Domain.Models;

public class StoredImage
{
    public int Id { get; set; }
    public int Title { get; set; }
    public byte[] Image { get; set; }
}
