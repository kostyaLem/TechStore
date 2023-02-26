﻿using TechStore.BL.Models;
using TechStore.BL.Models.CustomerModels;

namespace TechStore.BL.Services.Interfaces;

public interface ICustomerService
{
    Task<Customer> GetById(int id);

    Task<IReadOnlyList<Customer>> GetCustomers();

    Task Create(CreateCustomerRequest createRequest);

    Task Update(Customer customer);

    Task UpdateActiveStatus(IReadOnlyList<int> customerIds, bool isActive);

    Task Remove(int id);
}
