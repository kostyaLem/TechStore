namespace TechStore.DAL.Repositories.Models.Employees;

public record EmployeeDefinition
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public string PhoneNumber { get; init; }
    public DateTime Birthday { get; init; }
    public bool IsActive { get; init; }
}
