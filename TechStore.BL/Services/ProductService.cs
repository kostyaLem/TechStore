using TechStore.BL.Models.Products;
using TechStore.BL.Services.Interfaces;
using TechStore.DAL.Repositories.Interfaces;
using TechStore.DAL.Repositories.Models.Products;

namespace TechStore.BL.Services;

internal sealed class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task Create(CreateProductRequest createRequest)
    {
        var product = new ProductDefinition
        {
            Title = createRequest.Title,
            Description = createRequest.Description,
            Price = createRequest.Price,
            Image = createRequest.Image,
            CategoryId = createRequest.CategoryId,
            IsActive = createRequest.IsActive
        };

        await _productRepository.Create(product);
    }

    public async Task<Product> GetById(int id)
    {
        var requestedPromo = await _productRepository.GetProduct(id);

        return MapToUI(requestedPromo);
    }

    public async Task<IReadOnlyList<Product>> GetProducts()
    {
        var promos = await _productRepository.GetProducts();
        return promos.Select(MapToUI).ToList();
    }

    public async Task<Product> Update(Product product)
    {
        var updated = await _productRepository.Update(product.Id,
            new ProductDefinition
            {
                Title = product.Title,
                Image = product.Image,
                Description = product.Description,
                IsActive = product.IsActive,
                Price = product.Price,
                CategoryId = product.Category.Id
            });

        return MapToUI(updated);
    }

    public async Task UpdateActiveStatus(IReadOnlyList<int> productIds, bool isActive)
    {
        await _productRepository.ChangeActiveStatus(productIds, isActive);
    }

    private static Product MapToUI(RequestedProduct requestedProduct)
    {
        return new Product
        {
            Id = requestedProduct.Id,
            Title = requestedProduct.Title,
            Description = requestedProduct.Description,
            Image = requestedProduct.Image,
            Price= requestedProduct.Price,
            SalesCount = requestedProduct.SalesCount,
            IsActive = requestedProduct.IsActive,
            Category = new Models.Categories.Category
            {
                Id = requestedProduct.Category.Id,
                Name = requestedProduct.Category.Name,
                Products = Array.Empty<string>(),
            }
        };
    }
}
