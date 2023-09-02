using TechStore.BL.Models.Categories;

namespace TechStore.BL.Mapping;

public static class CategoryMapper
{
    public static CreateCategoryRequest MapToRequest(this Category category)
    {
        return new CreateCategoryRequest
        {
            Name = category.Name
        };
    }
}
