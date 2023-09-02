using TechStore.BL.Models.Categories;

namespace TechStore.BL.Services.Interfaces;

internal interface ICategoryService
{
    Task Create(CreateCategoryRequest createRequest);
    Task<Category> GetById(int id);
    Task<IReadOnlyList<Category>> GetCategories();
    Task Remove(IReadOnlyList<int> categoryIds);
    Task<Category> Update(Category category);
}