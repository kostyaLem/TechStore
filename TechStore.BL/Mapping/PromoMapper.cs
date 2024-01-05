using TechStore.BL.Models.Promos;

namespace TechStore.BL.Mapping;

public static class PromoMapper
{
    public static CreatePromoRequest MapToRequest(this Promo promo, int employeeId, IReadOnlyList<int> categories)
    {
        return new CreatePromoRequest
        {
            Name = promo.Name,
            Discount = promo.Discount,
            CreatedByUserId = employeeId,
            CategoriesIds = categories
        };
    }
}
