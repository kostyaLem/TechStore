using TechStore.DAL.Repositories.Models;
using TechStore.DAL.Repositories.Models.Customers;
using TechStore.Domain.Models;

namespace TechStore.DAL.Mapping;

public static class CustomerMapper
{
    public static RequestedCustomer MapToBL(this Customer customer)
    {
        var orders = customer.Orders
            .Select(x => new OrderDetails { OrderId = x.Id })
            .ToList();

        var result = new RequestedCustomer
        {
            Id = customer.Id,
            Login = customer.User.Login,
            Email = customer.Email,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Phone = customer.Phone,
            Birthday = customer.Birthday,
            UpdateOn = customer.User.UpdatedOn,
            IsActive = customer.User.IsActive,
            Orders = orders
        };

        return result;
    }
}
