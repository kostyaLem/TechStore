using TechStore.BL.Models;

namespace TechStore.BL.Services.Interfaces;

public interface ICustomerService
{
    Task<IReadOnlyList<Customer>> GetCustomers();

    Task Create(Customer customer);

    Task Update(Customer customer);

    Task UpdateActiveStatus(IReadOnlyList<int> customerIds, bool isActive);

    Task Remove(IReadOnlyList<int> customerIds);
}
