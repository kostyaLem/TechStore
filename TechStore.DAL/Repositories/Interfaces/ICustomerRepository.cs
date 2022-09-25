using TechStore.DAL.Repositories.Models.Customers;

namespace TechStore.DAL.Repositories.Interfaces;

public interface ICustomerRepository
{
    Task Create(CreateCustomerRequest customer);
    Task<IReadOnlyList<RequestedCustomer>> GetCustomers();
    Task Remove(int customerId);
    Task SetActiveStatus(IReadOnlyList<int> customerIds, bool isActive);
    Task Update(UpdateCustomerRequest request);
}