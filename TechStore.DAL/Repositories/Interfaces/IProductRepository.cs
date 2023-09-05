using TechStore.DAL.Repositories.Models.Products;

namespace TechStore.DAL.Repositories.Interfaces;

public interface IProductRepository
{
    Task ChangeActiveStatus(IReadOnlyList<int> productIds, bool isActive);
    Task Create(ProductDefinition product);
    Task<RequestedProduct> GetProduct(int productId);
    Task<IReadOnlyList<RequestedProduct>> GetProducts();
    Task<RequestedProduct> Update(int productId, ProductDefinition updated);
}