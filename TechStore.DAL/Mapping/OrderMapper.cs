using TechStore.DAL.Repositories.Models;
using TechStore.Domain.Models;

namespace TechStore.DAL.Mapping;

public static class OrderMapper
{
    public static OrderDetails MapToBL(this Order order)
    {
        var result = new OrderDetails
        {
            OrderId = order.Id,
            CreatedAt = order.CreatedOn,
            Customer = CustomerMapper.MapToBL(order.Customer),
            Employee = EmployeeMapper.MapToBL(order.Employee),
            Products = order.OrderProducts.Select(OrderProductMapper.MapToBl).ToList(),
        };

        return result;
    }
}
