using TechStore.BL.Models.Employees;

namespace TechStore.BL.Services.Interfaces;

public interface IEmployeeService
{
    Task<Employee> GetById(int id);

    Task<IReadOnlyList<Employee>> GetEmployees();

    Task Create(CreateEmployeeRequest createRequest);

    Task<Employee> Update(Employee customer, string password);

    Task UpdateActiveStatus(IReadOnlyList<int> employeeIds, bool isActive);

    Task Remove(IReadOnlyList<int> employeeIds);
}
