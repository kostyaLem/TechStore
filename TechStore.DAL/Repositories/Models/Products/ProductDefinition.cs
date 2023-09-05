namespace TechStore.DAL.Repositories.Models.Products;

public class ProductDefinition
{
    public string Title { get; init; }
    public string Description { get; init; }
    public decimal Price { get; init; }
    public byte[]? Image { get; init; }
    public int? CategoryId { get; init; }
    public bool IsActive { get; init; }
}
