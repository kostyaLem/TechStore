﻿using TechStore.DAL.Repositories.Models.Customers;
using TechStore.Domain.Models;

namespace TechStore.DAL.Mapping;

public static partial class Mapper
{
    public static RequestedCustomer MapToBL(this Customer customer)
    {
        var orders = customer.Orders.Select(x =>
        {
            return new OrderDetails
            {
                OrderId = x.Id
            };
        }).ToList();

        var result = new RequestedCustomer
        {
            Id = customer.Id,
            Email = customer.Email,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Phone = customer.Phone,
            Birthday = customer.Birthday,
            UpdateOn = customer.UpdatedOn,
            IsActive = customer.IsActive,
            Orders = orders
        };

        return result;
    }
}