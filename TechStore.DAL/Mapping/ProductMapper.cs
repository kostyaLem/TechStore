using TechStore.DAL.Repositories.Models.Categories;
using TechStore.DAL.Repositories.Models.Products;
using TechStore.Domain.Models;

namespace TechStore.DAL.Mapping;

public static class ProductMapper
{
    public static RequestedProduct MapToBl(this Product product)
    {
        RequestedCategory category = default!;

        if (product.CategoryId != null)
        {
            category = new RequestedCategory()
            {
                Id = product.Category.Id,
                Name = product.Category.Name,
                Products = Array.Empty<string>()
            };
        }

        return new RequestedProduct
        {
            Id = product.Id,
            Title = product.Title,
            Description = product.Description,
            Price = product.Price,
            CreatedOn = product.CreatedOn,
            Image = product.Image,
            SmallImage = product.SmallImage,
            SalesCount = product.OrderProducts.Count,
            Category = category,
            IsActive = product.IsActive
        };
    }
}
