using TechStore.BL.Models.Products;

namespace TechStore.BL.Services.Interfaces;

public interface IProductService
{
    Task Create(CreateProductRequest createRequest);
    Task<Product> GetById(int id);
    Task<IReadOnlyList<Product>> GetProducts();
    Task<Product> Update(Product product);
    Task UpdateActiveStatus(IReadOnlyList<int> productIds, bool isActive);
}