using TechStore.BL.Models.Products;
using Product = TechStore.BL.Models.Products.Product;

namespace TechStore.BL.Mapping;

public static class ProductMapper
{
    public static CreateProductRequest MapToRequest(this Product product)
    {
        return new CreateProductRequest()
        {
            Title = product.Title,
            Description = product.Description,
            Price = product.Price,
            CategoryId = product.Category.Id,
            Image = product.Image,
            SmallImage = product.SmallImage,
            IsActive = product.IsActive,
        };
    }
}