using TechStore.BL.Models.Customers;

namespace TechStore.BL.Mapping;

public static class CustomerMapper
{
    public static CreateCustomerRequest MapToRequest(this Customer customer, string password)
    {
        return new CreateCustomerRequest
        {
            Login = customer.Login,
            Password = password,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            PhoneNumber = customer.Phone,
            Birthday = customer.Birthday,
            Email = customer.Email
        };
    }
}
