namespace TechStore.DAL.Repositories.Models.Customers;

public class CreateCustomerRequest
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public string Phone { get; init; }
    public DateTime Birthday { get; init; }
}
