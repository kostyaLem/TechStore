﻿using TechStore.DAL.Repositories.Models.Customers;

namespace TechStore.DAL.Repositories.Interfaces;

public interface ICustomerRepository
{
    Task Create(CustomerDefenition customer, Credentials credentials);
    Task<IReadOnlyList<RequestedCustomer>> GetCustomers();
    Task<RequestedCustomer> GetCustomer(int customerId);
    Task Remove(IReadOnlyList<int> customerIds);
    Task SetActiveStatus(IReadOnlyList<int> customerIds, bool isActive);
    Task<RequestedCustomer> Update(int id, CustomerDefenition updated, Credentials credentials);
}