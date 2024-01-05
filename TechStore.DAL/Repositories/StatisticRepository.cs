using Microsoft.EntityFrameworkCore;
using TechStore.DAL.Context;
using TechStore.DAL.Repositories.Interfaces;
using TechStore.DAL.Repositories.Models;

namespace TechStore.DAL.Repositories;

internal sealed class StatisticRepository : IStatisticRepository
{
    private readonly TechStoreContextFactory _dbContextFactory;

    public StatisticRepository(TechStoreContextFactory dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<CommonStatistic> CountCommonStatistic()
    {
        using var context = _dbContextFactory.CreateDbContext();

        return new CommonStatistic
        {
            OrdersCount = await context.Orders.CountAsync(),
            CategoriesCount = await context.Categories.CountAsync(),
            CustomersCount = await context.Customers.CountAsync(),
            EmployeesCount = await context.Employees.CountAsync(),
            ProductsCount = await context.Products.CountAsync(),
            PromosCount = await context.PromoCodes.CountAsync()
        };
    }
}
