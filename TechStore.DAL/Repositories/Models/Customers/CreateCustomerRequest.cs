namespace TechStore.DAL.Repositories.Models.Customers;

public record CustomerDefenition
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public string PhoneNumber { get; init; }
    public DateTime Birthday { get; init; }
    public bool IsActive { get; init; }
}
