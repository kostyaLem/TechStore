using TechStore.DAL.Repositories.Models.Categories;

namespace TechStore.DAL.Repositories.Models.Products;

public class RequestedProduct
{
    public int Id { get; init; }
    public string Title { get; init; }
    public decimal Price { get; init; }
    public DateTime CreatedOn { get; init; }
    public RequestedCategory Category { get; init; }
    public int SalesCount { get; init; }
    public bool IsActive { get; init; }
}
