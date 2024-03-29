﻿namespace TechStore.Domain.Models;

public class OrderProduct
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public double Discount { get; set; }

    public virtual Order Order { get; set; }
    public virtual Product Product { get; set; }
}
