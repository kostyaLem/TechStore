using TechStore.BL.Models;
using TechStore.BL.Models.CustomerModels;
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

    public async Task Create(CreateCustomerRequest createRequest)
    {
        var mappedCustomer = new CreateCustomer
        {
            FirstName = createRequest.FirstName,
            LastName = createRequest.LastName,
            Birthday = createRequest.Birthday,
            Email = createRequest.Email,
            PhoneNumber = createRequest.PhoneNumber
        };

        await _customerRepository.Create(mappedCustomer);
    }

    public Task<Customer> GetById(int id)
    {
        throw new NotImplementedException();
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

    public async Task Remove(IReadOnlyList<int> customerIds)
    {
        await _customerRepository.Remove(customerIds);
    }

    public Task Remove(int id)
    {
        throw new NotImplementedException();
    }

    public async Task Update(Customer customer)
    {
        await _customerRepository.Update(new UpdateCustomerRequest
        {
            Id = customer.Id,
            Email = customer.Email,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            PhoneNumber = customer.Phone,
            Birthday = customer.Birthday,
            IsActive = customer.IsActive
        });
    }

    public async Task UpdateActiveStatus(IReadOnlyList<int> customerIds, bool isActive)
    {
        await _customerRepository.SetActiveStatus(customerIds, isActive);
    }
}

internal class CustomerStatisticService : ICustomerStatisticService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerStatisticService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<CustomerStatistic> GetCustomerStatistic(int customerId)
    {
        var customer = await _customerRepository.GetCustomer(customerId);
        return null;
    }
}