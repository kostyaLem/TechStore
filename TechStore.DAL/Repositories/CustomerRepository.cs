using Microsoft.EntityFrameworkCore;
using TechStore.DAL.Context;
using TechStore.DAL.Mapping;
using TechStore.DAL.Repositories.Interfaces;
using TechStore.DAL.Repositories.Models;
using TechStore.DAL.Repositories.Models.Customers;

namespace TechStore.DAL.Repositories;

internal sealed class CustomerRepository : ICustomerRepository
{
    private readonly TechStoreContextFactory _dbContextFactory;

    public CustomerRepository(TechStoreContextFactory dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<IReadOnlyList<RequestedCustomer>> GetCustomers()
    {
        using var context = _dbContextFactory.CreateDbContext();

        var customers = await context.Customers
            .Include(x => x.Orders)
            .Include(x => x.User)
            .AsNoTracking()
            .ToListAsync();

        return customers.Select(CustomerMapper.MapToBL).ToList();
    }

    public async Task Create(CustomerDefinition customer, Credentials credentials)
    {
        using var context = _dbContextFactory.CreateDbContext();

        var now = DateTime.UtcNow;

        var newCustomer = new Domain.Models.Customer
        {
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Birthday = customer.Birthday,
            Email = customer.Email,
            Phone = customer.PhoneNumber,            
            User = new Domain.Models.User()
            {
                Login = credentials.Login,
                PasswordHash = credentials.PasswordHash,
                UpdatedOn = now,
                Type = Domain.Models.UserType.Customer,
                CreatedOn = now,
                LastActivity = now,
                IsActive = true
            }
        };

        await context.Customers.AddAsync(newCustomer);
        await context.SaveChangesAsync();
    }

    public async Task<RequestedCustomer> Update(int id, CustomerDefinition updated, Credentials credentials)
    {
        using var context = _dbContextFactory.CreateDbContext();

        var customer = await context.Customers
            .Include(x => x.User)
            .FirstAsync(x => x.Id == id);

        customer.Email = updated.Email;
        customer.FirstName = updated.FirstName;
        customer.LastName = updated.LastName;
        customer.Phone = updated.PhoneNumber;
        customer.Birthday = updated.Birthday;

        customer.User.Login = credentials.Login;
        customer.User.UpdatedOn = DateTime.Now;
        customer.User.IsActive = updated.IsActive;

        if (!string.IsNullOrWhiteSpace(credentials.PasswordHash))
            customer.User.PasswordHash = credentials.PasswordHash;

        await context.SaveChangesAsync();

        return customer.MapToBL();
    }

    public async Task SetActiveStatus(IReadOnlyList<int> customerIds, bool isActive)
    {
        using var context = _dbContextFactory.CreateDbContext(); 
        
        var customers = await context.Customers
            .Include(x => x.User)
            .Where(x => customerIds.Contains(x.Id))
            .ToListAsync();

        customers.ForEach(x => x.User.IsActive = isActive);
        await context.SaveChangesAsync();
    }

    public async Task Remove(IReadOnlyList<int> customerIds)
    {
        using var context = _dbContextFactory.CreateDbContext();

        var customers = await context.Customers
            .Where(x => customerIds.Contains(x.Id))
            .ToListAsync();

        context.RemoveRange(customers);
        await context.SaveChangesAsync();
    }

    public async Task<RequestedCustomer> GetCustomer(int customerId)
    {
        using var context = _dbContextFactory.CreateDbContext();

        var customer = await context.Customers
            .Include(x => x.User)
            .AsNoTracking()
            .FirstAsync(x => x.Id == customerId);

        return customer!.MapToBL();
    }
}
