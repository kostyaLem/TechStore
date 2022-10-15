using Microsoft.EntityFrameworkCore;
using TechStore.DAL.Context;
using TechStore.DAL.Repositories.Interfaces;
using TechStore.DAL.Repositories.Models.Customers;

namespace TechStore.DAL.Repositories;

internal class CustomerRepository : ICustomerRepository
{
    private readonly TechStoreContext _context;

    public CustomerRepository(TechStoreContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<RequestedCustomer>> GetCustomers()
    {
        var customers = await _context.Customers.AsNoTracking().ToListAsync();

        return customers.Select(x => new RequestedCustomer()
        {
            Id = x.Id,
            Email = x.Email,
            FirstName = x.FirstName,
            LastName = x.LastName,
            Phone = x.Phone,
            Birthday = x.Birthday,
            UpdateOn = x.UpdatedOn,
            IsActive = x.IsActive
        }).ToList();
    }

    public async Task Create(CreateCustomerRequest request)
    {
        var newCustomer = new Domain.Models.Customer
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Birthday = request.Birthday,
            Email = request.Email,
            Phone = request.Phone,
            UpdatedOn = DateTime.Now,
            IsActive = true
        };

        await _context.Customers.AddAsync(newCustomer);
        await _context.SaveChangesAsync();
    }

    public async Task Update(UpdateCustomerRequest request)
    {
        var customer = await _context.Customers.FindAsync(request.Id);

        customer!.Id = request.Id;
        customer.Email = request.Email;
        customer.FirstName = request.FirstName;
        customer.LastName = request.LastName;
        customer.Phone = request.Phone;
        customer.Birthday = request.Birthday;
        customer.IsActive = request.IsActive;
        customer.UpdatedOn = DateTime.Now;

        await _context.SaveChangesAsync();
    }

    public async Task SetActiveStatus(IReadOnlyList<int> customerIds, bool isActive)
    {
        var customers = await _context.Customers.Where(x => customerIds.Contains(x.Id))
            .ToListAsync();

        customers.ForEach(x => x.IsActive = isActive);
        await _context.SaveChangesAsync();
    }

    public async Task Remove(IReadOnlyList<int> customerIds)
    {
        var customers = await _context.Customers
            .Where(x => customerIds.Contains(x.Id))
            .ToListAsync();

        _context.RemoveRange(customers);
        await _context.SaveChangesAsync();
    }
}
