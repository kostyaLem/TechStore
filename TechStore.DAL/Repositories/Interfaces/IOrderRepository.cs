using TechStore.DAL.Repositories.Models;

namespace TechStore.DAL.Repositories.Interfaces;

public interface IOrderRepository
{
    Task<IReadOnlyList<OrderDetails>> GetOrders();
}