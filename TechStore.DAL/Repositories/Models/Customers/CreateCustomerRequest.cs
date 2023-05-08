namespace TechStore.DAL.Repositories.Models.Customers;

public class CreateCustomer
{
    public string Login { get; init; }
    public string Password { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public string PhoneNumber { get; init; }
    public DateTime Birthday { get; init; }
}
