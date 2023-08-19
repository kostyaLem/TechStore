namespace TechStore.BL.Models.Customers;

public sealed class CreateCustomerRequest
{
    public string Login { get; set; }
    public string Password { get; set; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public string PhoneNumber { get; init; }
    public DateTime Birthday { get; init; }
}