using TechStore.BL.Models.Promos;

namespace TechStore.BL.Services.Interfaces;

public interface IPromoService
{
    Task Create(CreatePromoRequest createRequest);
    Task<Promo> GetById(int id);
    Task<IReadOnlyList<Promo>> GetPromos();
    Task<Promo> Update(Promo promo, IReadOnlyList<int> categoriesIds);
    Task UpdateActiveStatus(IReadOnlyList<int> promoIds, bool isActive);
    Task Remove(IReadOnlyList<int> promoIds);
}