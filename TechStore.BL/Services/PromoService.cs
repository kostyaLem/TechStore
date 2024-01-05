using TechStore.BL.Models.Promos;
using TechStore.BL.Services.Interfaces;
using TechStore.DAL.Repositories.Interfaces;
using TechStore.DAL.Repositories.Models.Promos;

namespace TechStore.BL.Services;

internal sealed class PromoService : IPromoService
{
    private readonly IPromoRepository _promoRepository;

    public PromoService(IPromoRepository promoRepository)
    {
        _promoRepository = promoRepository;
    }

    public async Task Create(CreatePromoRequest createRequest)
    {
        var mappedPromo = new PromoDefinition
        {
            Name = createRequest.Name,
            Discount = createRequest.Discount,
            CategoriesIds = createRequest.CategoriesIds,
            CreatedByUserId = createRequest.CreatedByUserId,
        };

        await _promoRepository.Create(mappedPromo);
    }

    public async Task<Promo> GetById(int id)
    {
        var requestedPromo = await _promoRepository.GetPromo(id);

        return MapToUI(requestedPromo);
    }

    public async Task<IReadOnlyList<Promo>> GetPromos()
    {
        var promos = await _promoRepository.GetPromos();
        return promos.Select(MapToUI).ToList();
    }

    public async Task<Promo> Update(Promo promo, IReadOnlyList<int> categoriesIds)
    {
        var updated = await _promoRepository.Update(promo.Id,
            new PromoDefinition
            {
                Name = promo.Name,
                Discount = promo.Discount,
                IsActive = promo.IsActive,
                CategoriesIds = categoriesIds
            });

        return MapToUI(updated);
    }

    public async Task UpdateActiveStatus(IReadOnlyList<int> promoIds, bool isActive)
    {
        await _promoRepository.ChangeActiveStatus(promoIds, isActive);
    }

    public async Task Remove(IReadOnlyList<int> promoIds)
    {
        await _promoRepository.Remove(promoIds);
    }

    private static Promo MapToUI(RequestedPromo requestedPromo)
    {
        return new Promo
        {
            Id = requestedPromo.Id,
            Name = requestedPromo.Name,
            Discount = requestedPromo.Discount,
            CreatedOn = requestedPromo.CreatedOn,
            IsActive = requestedPromo.IsActive,
            Categories = requestedPromo.Categories.Select(x => new Models.Categories.Category { Id = x.Id, Name = x.Name }).ToList(),
            CreatedBy = $"{requestedPromo.Employee.FirstName} {requestedPromo.Employee.LastName}"
        };
    }
}
