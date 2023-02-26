using TechStore.BL.Models;
using TechStore.BL.Models.CustomerModels;

namespace TechStore.BL.Mapping;

public static class CustomerMapper
{
    public static CreateCustomerRequest MapToRequest(this Customer customer)
    {
        return new CreateCustomerRequest
        {
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            PhoneNumber = customer.Phone,
            Birthday = customer.Birthday,
            Email = customer.Email
        };
    }
}
