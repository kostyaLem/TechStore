using Microsoft.EntityFrameworkCore;
using TechStore.DAL.Context;
using TechStore.DAL.Mapping;
using TechStore.DAL.Repositories.Interfaces;
using TechStore.DAL.Repositories.Models.Products;
using TechStore.Domain.Models;

namespace TechStore.DAL.Repositories;

internal class ProductRepository : IProductRepository
{
    private readonly TechStoreContextFactory _dbContextFactory;

    public ProductRepository(TechStoreContextFactory dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<IReadOnlyList<RequestedProduct>> GetProducts()
    {
        using var context = _dbContextFactory.CreateDbContext();

        var products = await context.Products
            .Include(x => x.OrderProducts)
            .Include(x => x.Category)
            .AsNoTracking()
            .ToListAsync();

        return products.Select(ProductMapper.MapToBl).ToList();
    }

    public async Task Create(ProductDefinition product)
    {
        using var context = _dbContextFactory.CreateDbContext();

        var newProduct = new Product
        {
            Title = product.Title,
            Description = product.Description,
            Price = product.Price,
            CreatedOn = DateTime.UtcNow,
            IsActive = product.IsActive,
            Image = product.Image ?? null,
            CategoryId = product.CategoryId
        };

        await context.AddAsync(newProduct);
        await context.SaveChangesAsync();
    }

    public async Task<RequestedProduct> Update(int productId, ProductDefinition updated)
    {
        using var context = _dbContextFactory.CreateDbContext();

        var product = await context.Products
            .FirstAsync(c => c.Id == productId);

        product.Title = updated.Title;
        product.Description = updated.Description;
        product.Price = updated.Price;
        product.CategoryId = updated.CategoryId;
        product.Image = updated.Image;
        product.IsActive = updated.IsActive;

        await context.SaveChangesAsync();
        return ProductMapper.MapToBl(product);
    }

    public async Task<RequestedProduct> GetProduct(int productId)
    {
        using var context = _dbContextFactory.CreateDbContext();

        var product = await context.Products
            .Include(x => x.OrderProducts)
            .Include(x => x.Category)
            .AsNoTracking()
            .FirstAsync(x => x.Id == productId);

        return ProductMapper.MapToBl(product);
    }

    public async Task ChangeActiveStatus(IReadOnlyList<int> productIds, bool isActive)
    {
        using var context = _dbContextFactory.CreateDbContext();

        var promos = await context.Products
            .Where(x => productIds.Contains(x.Id))
            .ToListAsync();

        promos.ForEach(x => x.IsActive = isActive);
        await context.SaveChangesAsync();
    }
}
