using TechStore.BL.Auth;
using TechStore.BL.Models.Employees;
using TechStore.BL.Services.Interfaces;
using TechStore.DAL.Repositories.Interfaces;
using TechStore.DAL.Repositories.Models.Employees;

namespace TechStore.BL.Services;

internal class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeService(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task Create(CreateEmployeeRequest createRequest)
    {
        var mappedEmployee = new EmployeeDefinition
        {
            FirstName = createRequest.FirstName,
            LastName = createRequest.LastName,
            Birthday = createRequest.Birthday,
            Email = createRequest.Email,
            PhoneNumber = createRequest.PhoneNumber
        };

        await _employeeRepository.Create(
            mappedEmployee,
            new(createRequest.Login, HashService.Compute(createRequest.Password)));
    }

    public async Task<Employee> GetById(int id)
    {
        var requestedCustomer = await _employeeRepository.GetEmployee(id);

        return MapToUI(requestedCustomer);
    }

    public async Task<IReadOnlyList<Employee>> GetEmployees()
    {
        var employees = await _employeeRepository.GetEmployees();
        return employees.Select(MapToUI).ToList();
    }

    public async Task Remove(IReadOnlyList<int> employeeIds)
    {
        await _employeeRepository.Remove(employeeIds);
    }

    public async Task<Employee> Update(Employee employee, string password)
    {
        var updatedEmployee = await _employeeRepository.Update(employee.Id,
            new EmployeeDefinition()
            {
                Email = employee.Email,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                PhoneNumber = employee.Phone,
                Birthday = employee.Birthday,
                IsActive = employee.IsActive
            },
            new(employee.Login, HashService.Compute(password)));

        return MapToUI(updatedEmployee);
    }

    public async Task UpdateActiveStatus(IReadOnlyList<int> employeeIds, bool isActive)
    {
        await _employeeRepository.SetActiveStatus(employeeIds, isActive);
    }

    private static Employee MapToUI(RequestedEmployee rEmployee)
    {
        return new Employee
        {
            Id = rEmployee.Id,
            Login = rEmployee.Login,
            Birthday = rEmployee.Birthday,
            FirstName = rEmployee.FirstName,
            LastName = rEmployee.LastName,
            Email = rEmployee.Email,
            IsActive = rEmployee.IsActive,
            Phone = rEmployee.Phone,
            UpdatedOn = rEmployee.UpdateOn
        };
    }
}
