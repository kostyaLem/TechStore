namespace TechStore.DAL.Repositories.Models.Customers;

public sealed class UpdateCustomerRequest : CreateCustomer
{
    public int Id { get; init; }
    public string PasswordHash { get; init; }
    public bool IsActive { get; init; }
}
