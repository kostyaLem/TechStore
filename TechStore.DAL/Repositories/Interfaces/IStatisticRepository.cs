using TechStore.DAL.Repositories.Models;

namespace TechStore.DAL.Repositories.Interfaces;

public interface IStatisticRepository
{
    Task<CommonStatistic> CountCommonStatistic();
}