namespace TechStore.DAL.Repositories.Models.Categories;

public class RequestedCategory
{
    public int Id { get; init; }
    public string Name { get; init; }
    public IReadOnlyList<string> Products { get; init; }
}
