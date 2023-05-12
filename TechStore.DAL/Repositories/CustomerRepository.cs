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

    public async Task Create(CreateCustomer request)
    {
        var newCustomer = new Domain.Models.Customer
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Birthday = request.Birthday,
            Email = request.Email,
            Phone = request.PhoneNumber,
            IsActive = true,
            User = new Domain.Models.User()
            {
                Login = request.Login,
                PasswordHash = request.Password,
                UpdatedOn = DateTime.Now
            }
        };

        await _context.Customers.AddAsync(newCustomer);
        await _context.SaveChangesAsync();
    }

    public async Task<RequestedCustomer> Update(UpdateCustomerRequest request)
    {
        var customer = await _context.Customers
            .Include(x => x.User)
            .AsNoTracking()
            .FirstAsync(x => x.Id == request.Id);

        customer.Id = request.Id;
        customer.Email = request.Email;
        customer.FirstName = request.FirstName;
        customer.LastName = request.LastName;
        customer.Phone = request.PhoneNumber;
        customer.Birthday = request.Birthday;
        customer.IsActive = request.IsActive;

        customer.User.Login = request.Login;

        if (!string.IsNullOrWhiteSpace(request.PasswordHash))
            customer.User.PasswordHash = request.PasswordHash;

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
