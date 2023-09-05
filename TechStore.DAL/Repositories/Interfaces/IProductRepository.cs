using TechStore.DAL.Repositories.Models.Products;

namespace TechStore.DAL.Repositories.Interfaces;

internal interface IProductRepository
{
    Task Create(ProductDefinition product);
    Task<RequestedProduct> GetProduct(int productId);
    Task<IReadOnlyList<RequestedProduct>> GetProducts();
    Task<RequestedProduct> Update(int productId, ProductDefinition updated);
}