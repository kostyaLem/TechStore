namespace TechStore.DAL.Repositories.Models.Products;

public class ProductDefinition
{
    public string Title { get; init; }
    public string Description { get; internal set; }
    public decimal Price { get; init; }
    public byte[]? Image { get; init; }
    public int? CategoryId { get; internal set; }
    public bool IsActive { get; init; }
}
