using TechStore.BL.Models.Categories;
using TechStore.BL.Services.Interfaces;
using TechStore.DAL.Repositories.Interfaces;
using TechStore.DAL.Repositories.Models.Categories;

namespace TechStore.BL.Services;

internal sealed class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task Create(CreateCategoryRequest createRequest)
        => await _categoryRepository.Create(new() { Name = createRequest.Name });

    public async Task<Category> GetById(int id)
    {
        var requestedCategory = await _categoryRepository.GetCategory(id);

        return MapToUI(requestedCategory);
    }

    public async Task<IReadOnlyList<Category>> GetCategories()
    {
        var categories = await _categoryRepository.GetCategories();

        return categories.Select(MapToUI).ToList();
    }

    public async Task<Category> Update(Category category)
    {
        var updated = await _categoryRepository.Update(category.Id,
            new CategoryDefinition
            {
                Name = category.Name
            });

        return MapToUI(updated);
    }

    public async Task Remove(IReadOnlyList<int> categoryIds)
    {
        await _categoryRepository.Remove(categoryIds);
    }

    private static Category MapToUI(RequestedCategory requestedCategory)
    {
        return new Category
        {
            Name = requestedCategory.Name
        };
    }
}
