using TechStore.DAL.Repositories.Models.Promos;

namespace TechStore.DAL.Repositories.Interfaces;

public interface IPromoRepository
{
    Task ChangeActiveStatus(IReadOnlyList<int> promoIds, bool isActive);
    Task Create(PromoDefinition promo);
    Task<IReadOnlyList<RequestedPromo>> GetActivePromos();
    Task<RequestedPromo> GetPromo(int id);
    Task<IReadOnlyList<RequestedPromo>> GetPromos();
    Task Remove(IReadOnlyList<int> promoIds);
    Task<RequestedPromo> Update(int id, PromoDefinition updated);
}