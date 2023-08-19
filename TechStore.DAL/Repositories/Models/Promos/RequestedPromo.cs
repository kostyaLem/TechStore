using TechStore.DAL.Repositories.Models.Employees;

namespace TechStore.DAL.Repositories.Models.Promos;

public class RequestedPromo
{
    public int Id { get; init; }
    public string Name { get; init; }
    public double Discount { get; init; }
    public DateTime CreatedOn { get; init; }
    public bool IsActive { get; init; }
    public RequestedEmployee Employee { get; init; }
}
