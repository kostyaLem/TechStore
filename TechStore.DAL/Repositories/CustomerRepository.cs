using Microsoft.EntityFrameworkCore;
using TechStore.DAL.Context;
using TechStore.DAL.Mapping;
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
        var customers = await _context.Customers
            .Include(x => x.Orders)
            .Include(x => x.User)
            .AsNoTracking()
            .ToListAsync();

        return customers.Select(Mapper.MapToBL).ToList();
    }

    public async Task Create(CustomerDefenition customer, Credentials credentials)
    {
        var newCustomer = new Domain.Models.Customer
        {
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Birthday = customer.Birthday,
            Email = customer.Email,
            Phone = customer.PhoneNumber,
            IsActive = true,
            User = new Domain.Models.User()
            {
                Login = credentials.Login,
                PasswordHash = credentials.PasswordHash,
                UpdatedOn = DateTime.Now
            }
        };

        await _context.Customers.AddAsync(newCustomer);
        await _context.SaveChangesAsync();
    }

    public async Task<RequestedCustomer> Update(int id, CustomerDefenition updated, Credentials credentials)
    {
        var customer = await _context.Customers
            .Include(x => x.User)
            .FirstAsync(x => x.Id == id);

        customer.Email = updated.Email;
        customer.FirstName = updated.FirstName;
        customer.LastName = updated.LastName;
        customer.Phone = updated.PhoneNumber;
        customer.Birthday = updated.Birthday;
        customer.IsActive = updated.IsActive;

        customer.User.Login = credentials.Login;
        customer.User.UpdatedOn = DateTime.Now;

        if (!string.IsNullOrWhiteSpace(credentials.PasswordHash))
            customer.User.PasswordHash = credentials.PasswordHash;

        await _context.SaveChangesAsync();

        return customer.MapToBL();
    }

    public async Task SetActiveStatus(IReadOnlyList<int> customerIds, bool isActive)
    {
        var customers = await _context.Customers
            .Where(x => customerIds.Contains(x.Id))
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

    public async Task<RequestedCustomer> GetCustomer(int customerId)
    {
        var customer = await _context.Customers
            .Include(x => x.User)
            .AsNoTracking()
            .FirstAsync(x => x.Id == customerId);

        return customer!.MapToBL();
    }
}
