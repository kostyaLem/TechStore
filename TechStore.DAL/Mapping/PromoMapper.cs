using TechStore.DAL.Repositories.Models.Promos;
using TechStore.Domain.Models;

namespace TechStore.DAL.Mapping;

public static class PromoMapper
{
    public static RequestedPromo MapToBl(this PromoCode promoCode)
    {
        var result = new RequestedPromo
        {
            Id = promoCode.Id,
            Name = promoCode.Name,
            Discount = promoCode.Discount,
            CreatedOn = promoCode.CreatedOn,
            IsActive = promoCode.IsActive,
            Employee = EmployeeMapper.MapToBL(promoCode.Employee)
        };

        return result;
    }
}
