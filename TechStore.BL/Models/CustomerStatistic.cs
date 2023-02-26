namespace TechStore.BL.Models;

public record CustomerStatistic
{
    public int OrdersCount { get; }

    public decimal MinOrderAmount { get; }
    public decimal MaxOrderAmount { get; }

    public int MinOrderItemsCounts { get; }
    public int MaxOrderItemsCounts { get; }

    public DateTime FirstOrderDate { get; }
    public DateTime LastOrderDate { get; }
}
