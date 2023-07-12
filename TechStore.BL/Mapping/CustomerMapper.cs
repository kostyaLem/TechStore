using TechStore.BL.Models.Customers;

namespace TechStore.BL.Mapping;

public static class CustomerMapper
{
    public static CreateCustomerRequest MapToRequest(this Customer customer, string passwordHash)
    {
        return new CreateCustomerRequest
        {
            Login = customer.Login,
            Password = passwordHash,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            PhoneNumber = customer.Phone,
            Birthday = customer.Birthday,
            Email = customer.Email
        };
    }
}
