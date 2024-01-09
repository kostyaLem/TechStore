using TechStore.DAL.Repositories.Models.Categories;
using TechStore.DAL.Repositories.Models.Customers;
using TechStore.DAL.Repositories.Models.Employees;

namespace TechStore.DAL.Repositories.Models.Products;

public class ProductDefinition
{
    public string Title { get; init; }
    public string Description { get; init; }
    public decimal Price { get; init; }
    public byte[]? Image { get; init; }
    public byte[]? SmallImage { get; init; }
    public bool IsActive { get; init; }

    public CategoryDefinition Category { get; init; }
}
