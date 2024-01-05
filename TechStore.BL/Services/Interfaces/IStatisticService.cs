using TechStore.BL.Models;

namespace TechStore.BL.Services.Interfaces;

public interface IStatisticService
{
    Task<Statistic> CountStatistic();
}