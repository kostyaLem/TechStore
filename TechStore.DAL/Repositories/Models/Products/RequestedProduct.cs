using TechStore.DAL.Repositories.Models.Categories;

namespace TechStore.DAL.Repositories.Models.Products;

public class RequestedProduct
{
    public int Id { get; init; }
    public string Title { get; init; }
    public string Description { get; init; }
    public decimal Price { get; init; }
    public byte[]? Image { get; init; }
    public byte[]? SmallImage { get; init; }
    public DateTime CreatedOn { get; init; }
    public RequestedCategory Category { get; init; }
    public int SalesCount { get; init; }
    public bool IsActive { get; init; }
}
