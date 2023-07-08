using TechStore.DAL.Repositories.Models;
using TechStore.DAL.Repositories.Models.Employees;

namespace TechStore.DAL.Repositories.Interfaces;
internal interface IEmployeeRepository
{
    Task Create(EmployeeDefinition employee, Credentials credentials);
    Task<RequestedEmployee> GetEmployee(int employeeId);
    Task<IReadOnlyList<RequestedEmployee>> GetEmployees();
    Task<RequestedEmployee> Update(int id, EmployeeDefinition updated, Credentials credentials);
}