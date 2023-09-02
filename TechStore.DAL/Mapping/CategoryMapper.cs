using TechStore.DAL.Repositories.Models.Categories;
using TechStore.Domain.Models;

namespace TechStore.DAL.Mapping;

public static class CategoryMapper
{
    public static RequestedCategory MapToBl(this Category category)
    {
        var result = new RequestedCategory
        {
            Id = category.Id,
            Name = category.Name
        };

        return result;
    }
}