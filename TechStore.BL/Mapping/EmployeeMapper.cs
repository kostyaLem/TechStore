using TechStore.BL.Models.Employees;

namespace TechStore.BL.Mapping;

public static class EmployeeMapper
{
    public static CreateEmployeeRequest MapToRequest(this Employee employee, string passwordHash)
    {
        return new CreateEmployeeRequest
        {
            Login = employee.Login,
            Password = passwordHash,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            PhoneNumber = employee.Phone,
            Birthday = employee.Birthday,
            Email = employee.Email
        };
    }
}
