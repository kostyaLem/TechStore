using TechStore.DAL.Repositories.Models.Products;
using TechStore.Domain.Models;

namespace TechStore.DAL.Mapping;

public static class OrderProductMapper
{
    public static RequestedOrderProduct MapToBl(this OrderProduct orderProduct)
    {
        var product = ProductMapper.MapToBl(orderProduct.Product);

        return new RequestedOrderProduct
        {
            OrderProductId = orderProduct.Id,
            Discount = orderProduct.Discount,
            Product = product
        };
    }
}