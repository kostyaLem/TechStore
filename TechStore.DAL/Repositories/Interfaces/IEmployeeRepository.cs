using TechStore.DAL.Repositories.Models;
using TechStore.DAL.Repositories.Models.Employees;

namespace TechStore.DAL.Repositories.Interfaces;

public interface IEmployeeRepository
{
    Task Create(EmployeeDefinition employee, Credentials credentials);
    Task<RequestedEmployee> GetEmployee(int employeeId);
    Task<IReadOnlyList<RequestedEmployee>> GetEmployees();
    Task<RequestedEmployee> Update(int id, EmployeeDefinition updated, Credentials credentials);
    Task Remove(IReadOnlyList<int> employeeIds);
    Task SetActiveStatus(IReadOnlyList<int> employeeOds, bool isActive);
}