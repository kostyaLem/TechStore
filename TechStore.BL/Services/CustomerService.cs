using TechStore.BL.Models;
using TechStore.BL.Services.Interfaces;
using TechStore.DAL.Repositories.Interfaces;
using TechStore.DAL.Repositories.Models.Customers;

namespace TechStore.BL.Services;

internal class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task Create(Customer customer)
    {
        var mappedCustomer = new CreateCustomerRequest
        {
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Birthday = customer.Birthday,
            Email = customer.Email,
            Phone = customer.Phone
        };

        await _customerRepository.Create(mappedCustomer);
    }

    public async Task<IReadOnlyList<Customer>> GetCustomers()
    {
        var customeres = await _customerRepository.GetCustomers();
        return customeres.Select(x => new Customer
        {
            Id = x.Id,
            Birthday = x.Birthday,
            FirstName = x.FirstName,
            LastName = x.LastName,
            Email = x.Email,
            IsActive = x.IsActive,
            Phone = x.Phone,
            UpdatedOn = x.UpdateOn
        }).ToList();
    }

    public async Task Remove(int customerId)
    {
        await _customerRepository.Remove(customerId);
    }

    public async Task Update(Customer customer)
    {
        await _customerRepository.Update(new UpdateCustomerRequest
        {
            Id = customer.Id,
            Email = customer.Email,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Phone = customer.Phone,
            Birthday = customer.Birthday,
            IsActive = customer.IsActive
        });
    }

    public async Task UpdateActiveStatus(IReadOnlyList<int> customerIds, bool isActive)
    {
        await _customerRepository.SetActiveStatus(customerIds, isActive);
    }
}
