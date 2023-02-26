using TechStore.BL.Models;

namespace TechStore.BL.Services.Interfaces;

public interface ICustomerStatisticService
{
    public Task<CustomerStatistic> GetCustomerStatistic(int customerId);
}