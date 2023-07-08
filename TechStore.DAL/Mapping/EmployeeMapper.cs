using TechStore.DAL.Repositories.Models.Employees;
using TechStore.Domain.Models;

namespace TechStore.DAL.Mapping;

public static class EmployeeMapper
{
    public static RequestedEmployee MapToBL(this Employee employee)
    {
        var result = new RequestedEmployee
        {
            Id = employee.Id,
            Login = employee.User.Login,
            Email = employee.Email,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Phone = employee.Phone,
            Birthday = employee.Birthday,
            UpdateOn = employee.User.UpdatedOn,
        };

        return result;
    }
}
