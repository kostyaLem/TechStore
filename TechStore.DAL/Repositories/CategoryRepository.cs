using Microsoft.EntityFrameworkCore;
using TechStore.DAL.Context;
using TechStore.DAL.Mapping;
using TechStore.DAL.Repositories.Interfaces;
using TechStore.DAL.Repositories.Models.Categories;
using TechStore.Domain.Models;

namespace TechStore.DAL.Repositories;

internal sealed class CategoryRepository : ICategoryRepository
{
    private readonly TechStoreContextFactory _dbContextFactory;

    public CategoryRepository(TechStoreContextFactory dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<IReadOnlyList<RequestedCategory>> GetCategories()
    {
        using var context = _dbContextFactory.CreateDbContext();

        var categories = await context.Categories
            .AsNoTracking()
            .ToListAsync();

        return categories.Select(CategoryMapper.MapToBl).ToList();
    }

    public async Task Create(CategoryDefinition category)
    {
        using var context = _dbContextFactory.CreateDbContext();

        var newCategory = new Category
        {
            Name = category.Name
        };

        await context.Categories.AddAsync(newCategory);
        await context.SaveChangesAsync();
    }

    public async Task<RequestedCategory> Update(int categoryId, CategoryDefinition updated)
    {
        using var context = _dbContextFactory.CreateDbContext();

        var category = await context.Categories
            .FirstAsync(c => c.Id == categoryId);

        category.Name = updated.Name;

        await context.SaveChangesAsync();
        return CategoryMapper.MapToBl(category);
    }

    public async Task<RequestedCategory> GetCategory(int categoryId)
    {
        using var context = _dbContextFactory.CreateDbContext();

        var category = await context.Categories
            .AsNoTracking()
            .FirstAsync(x => x.Id == categoryId);

        return CategoryMapper.MapToBl(category);
    }

    public async Task Remove(IReadOnlyList<int> categoryIds)
    {
        using var context = _dbContextFactory.CreateDbContext();

        var categories = await context.Categories
            .Where(x => categoryIds.Contains(x.Id))
            .ToListAsync();

        context.RemoveRange(categories);
        await context.SaveChangesAsync();
    }
}
