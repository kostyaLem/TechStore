using Microsoft.EntityFrameworkCore;
using TechStore.DAL.Context;
using TechStore.DAL.Mapping;
using TechStore.DAL.Repositories.Interfaces;
using TechStore.DAL.Repositories.Models;
using TechStore.DAL.Repositories.Models.Employees;

namespace TechStore.DAL.Repositories;

internal class EmployeeRepository : IEmployeeRepository
{
    private readonly TechStoreContextFactory _dbContextFactory;

    public EmployeeRepository(TechStoreContextFactory dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<IReadOnlyList<RequestedEmployee>> GetEmployees()
    {
        using var context = _dbContextFactory.CreateDbContext();

        var employees = await context.Employees
            .Include(x => x.User)
            .AsNoTracking()
            .ToListAsync();

        return employees.Select(EmployeeMapper.MapToBL).ToList();
    }

    public async Task Create(EmployeeDefinition employee, Credentials credentials)
    {
        using var context = _dbContextFactory.CreateDbContext();

        var now = DateTime.UtcNow;

        var newEmployee = new Domain.Models.Employee
        {
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Birthday = employee.Birthday,
            Email = employee.Email,
            Phone = employee.PhoneNumber,
            User = new Domain.Models.User()
            {
                Login = credentials.Login,
                PasswordHash = credentials.PasswordHash,
                UpdatedOn = now,
                Type = Domain.Models.UserType.Employee,
                CreatedOn = now,
                LastActivity = now,
                IsActive = true
            }
        };

        await context.Employees.AddAsync(newEmployee);
        await context.SaveChangesAsync();
    }

    public async Task<RequestedEmployee> Update(int id, EmployeeDefinition updated, Credentials credentials)
    {
        using var context = _dbContextFactory.CreateDbContext();

        var employee = await context.Employees
            .Include(x => x.User)
            .FirstAsync(x => x.Id == id);

        employee.Email = updated.Email;
        employee.FirstName = updated.FirstName;
        employee.LastName = updated.LastName;
        employee.Phone = updated.PhoneNumber;
        employee.Birthday = updated.Birthday;

        employee.User.Login = credentials.Login;
        employee.User.UpdatedOn = DateTime.Now;
        employee.User.IsActive = updated.IsActive;

        if (!string.IsNullOrWhiteSpace(credentials.PasswordHash))
            employee.User.PasswordHash = credentials.PasswordHash;

        await context.SaveChangesAsync();

        return employee.MapToBL();
    }

    public async Task<RequestedEmployee> GetEmployee(int employeeId)
    {
        using var context = _dbContextFactory.CreateDbContext();

        var customer = await context.Employees
            .Include(x => x.User)
            .AsNoTracking()
            .FirstAsync(x => x.Id == employeeId);

        return customer!.MapToBL();
    }

    public async Task Remove(IReadOnlyList<int> employeeIds)
    {
        using var context = _dbContextFactory.CreateDbContext();

        var employees = await context.Employees
            .Where(x => employeeIds.Contains(x.Id))
            .ToListAsync();

        context.RemoveRange(employees);
        await context.SaveChangesAsync();
    }

    public async Task SetActiveStatus(IReadOnlyList<int> employeeOds, bool isActive)
    {
        using var context = _dbContextFactory.CreateDbContext();

        var employees = await context.Employees
            .Include(x => x.User)
            .Where(x => employeeOds.Contains(x.Id))
            .ToListAsync();

        employees.ForEach(x => x.User.IsActive = isActive);
        await context.SaveChangesAsync();
    }
}
