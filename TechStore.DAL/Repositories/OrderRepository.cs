using Microsoft.EntityFrameworkCore;
using TechStore.DAL.Context;
using TechStore.DAL.Mapping;
using TechStore.DAL.Repositories.Interfaces;
using TechStore.DAL.Repositories.Models;

namespace TechStore.DAL.Repositories;

internal class OrderRepository : IOrderRepository
{
    private readonly TechStoreContextFactory _dbContextFactory;

    public OrderRepository(TechStoreContextFactory dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<IReadOnlyList<OrderDetails>> GetOrders()
    {
        using var context = _dbContextFactory.CreateDbContext();

        var orders = await context.Orders
            .Include(x => x.Customer)
            .Include(x => x.Employee)
            .Include(x => x.OrderProducts)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.Category)
            .ToListAsync();

        return orders.Select(OrderMapper.MapToBL).ToList();
    }
}
