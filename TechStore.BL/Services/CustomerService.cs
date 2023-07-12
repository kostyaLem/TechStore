using TechStore.BL.Auth;
using TechStore.BL.Models.Customers;
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
        var mappedCustomer = new CustomerDefinition
        {
            FirstName = createRequest.FirstName,
            LastName = createRequest.LastName,
            Birthday = createRequest.Birthday,
            Email = createRequest.Email,
            PhoneNumber = createRequest.PhoneNumber
        };

        await _customerRepository.Create(
            mappedCustomer,
            new(createRequest.Login, HashService.Compute(createRequest.Password)));
    }

    public async Task<Customer> GetById(int id)
    {
        var requestedCustomer = await _customerRepository.GetCustomer(id);

        return MapToUI(requestedCustomer);
    }

    public async Task<IReadOnlyList<Customer>> GetCustomers()
    {
        var customeres = await _customerRepository.GetCustomers();
        return customeres.Select(MapToUI).ToList();
    }

    public async Task Remove(IReadOnlyList<int> customerIds)
    {
        await _customerRepository.Remove(customerIds);
    }

    public Task Remove(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Customer> Update(Customer customer, string password)
    {
        var updatedCustomer = await _customerRepository.Update(customer.Id,
            new CustomerDefinition()
            {
                Email = customer.Email,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                PhoneNumber = customer.Phone,
                Birthday = customer.Birthday,
                IsActive = customer.IsActive
            },
            new(customer.Login, HashService.Compute(password)));

        return MapToUI(updatedCustomer);
    }

    public async Task UpdateActiveStatus(IReadOnlyList<int> customerIds, bool isActive)
    {
        await _customerRepository.SetActiveStatus(customerIds, isActive);
    }

    private static Customer MapToUI(RequestedCustomer rCustomer)
    {
        return new Customer
        {
            Id = rCustomer.Id,
            Login = rCustomer.Login,
            Birthday = rCustomer.Birthday,
            FirstName = rCustomer.FirstName,
            LastName = rCustomer.LastName,
            Email = rCustomer.Email,
            IsActive = rCustomer.IsActive,
            Phone = rCustomer.Phone,
            UpdatedOn = rCustomer.UpdateOn
        };
    }
}