using TechStore.DAL.Repositories.Models.Categories;

namespace TechStore.DAL.Repositories;
internal interface ICategoryRepository
{
    Task Create(CategoryDefinition category);
    Task<IReadOnlyList<RequestedCategory>> GetCategories();
    Task<RequestedCategory> GetCategory(int categoryId);
    Task Remove(IReadOnlyList<int> categoryIds);
    Task<RequestedCategory> Update(int categoryId, CategoryDefinition updated);
}