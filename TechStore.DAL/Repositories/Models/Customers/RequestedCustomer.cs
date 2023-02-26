namespace TechStore.DAL.Repositories.Models.Customers;

public class RequestedCustomer
{
    public int Id { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public string Phone { get; init; }
    public DateTime Birthday { get; init; }
    public DateTime UpdateOn { get; init; }
    public bool IsActive { get; set; }

    public IReadOnlyList<OrderDetails> Orders { get; init; }
}

public class OrderDetails
{
    public int OrderId { get; init; }
}