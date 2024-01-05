using TechStore.BL.Models;
using TechStore.BL.Services.Interfaces;
using TechStore.DAL.Repositories.Interfaces;
using TechStore.DAL.Repositories.Models;

namespace TechStore.BL.Services;

internal class StatisticService : IStatisticService
{
    private readonly IStatisticRepository _statisticRepository;

    public StatisticService(IStatisticRepository statisticRepository)
    {
        _statisticRepository = statisticRepository;
    }

    public async Task<Statistic> CountStatistic()
    {
        var statistic = await _statisticRepository.CountCommonStatistic();

        return MapToUI(statistic);
    }

    private static Statistic MapToUI(CommonStatistic commonStatistic)
    {
        return new Statistic
        {
            CategoriesCount = commonStatistic.CategoriesCount,
            CustomersCount = commonStatistic.CustomersCount,
            EmployeesCount = commonStatistic.EmployeesCount,
            OrdersCount = commonStatistic.OrdersCount,
            ProductsCount = commonStatistic.ProductsCount,
            PromosCount = commonStatistic.PromosCount
        };
    }
}
