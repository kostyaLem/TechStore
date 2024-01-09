using TechStore.DAL.Repositories.Models.Customers;
using TechStore.DAL.Repositories.Models.Employees;
using TechStore.DAL.Repositories.Models.Products;

namespace TechStore.DAL.Repositories.Models;

public class OrderDetails
{
    public int OrderId { get; init; }
    public DateTime CreatedAt { get; init; }

    public IReadOnlyList<RequestedOrderProduct> Products { get; init; }

    public RequestedCustomer Customer { get; init; }
    public RequestedEmployee Employee { get; init; }
}