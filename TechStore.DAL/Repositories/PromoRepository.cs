using Microsoft.EntityFrameworkCore;
using TechStore.DAL.Context;
using TechStore.DAL.Mapping;
using TechStore.DAL.Repositories.Interfaces;
using TechStore.DAL.Repositories.Models.Promos;
using TechStore.Domain.Models;

namespace TechStore.DAL.Repositories;

internal sealed class PromoRepository : IPromoRepository
{
    private readonly TechStoreContext _context;

    public PromoRepository(TechStoreContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<RequestedPromo>> GetPromos()
    {
        var promos = await _context.PromoCodes
            .Include(x => x.Employee)
            .ThenInclude(x => x.User)
            .AsNoTracking()
            .ToListAsync();

        return promos.Select(PromoMapper.MapToBl).ToList();
    }

    public async Task<IReadOnlyList<RequestedPromo>> GetActivePromos()
    {
        var promos = await _context.PromoCodes
            .Include(x => x.Employee)
            .ThenInclude(x => x.User)
            .Where(x => x.Active)
            .AsNoTracking()
            .ToListAsync();

        return promos.Select(PromoMapper.MapToBl).ToList();
    }

    public async Task Create(PromoDefinition promo)
    {
        var employee = _context.Employees
            .AsNoTracking()
            .First(x => x.UserId == promo.CreatedByUserId);

        var newPromo = new PromoCode
        {
            Name = promo.Name,
            Discount = promo.Discount,
            CreatedOn = DateTime.UtcNow,
            Active = true,
            CreatedByEmployeeId = employee.Id,
        };

        await _context.PromoCodes.AddAsync(newPromo);
        await _context.SaveChangesAsync();
    }

    public async Task<RequestedPromo> Update(int promoId, PromoDefinition updated)
    {
        var promo = await _context.PromoCodes
            .Include(x => x.Employee)
                .ThenInclude(x => x.User)
            .FirstAsync(x => x.Id == promoId);

        promo.Name = updated.Name;
        promo.Discount = updated.Discount;
        promo.Active = updated.IsActive;

        await _context.SaveChangesAsync();

        return PromoMapper.MapToBl(promo);
    }

    public async Task ChangeActiveStatus(IReadOnlyList<int> promoIds, bool isActive)
    {
        var promos = await _context.PromoCodes
            .Where(x => promoIds.Contains(x.Id))
            .ToListAsync();

        promos.ForEach(x => x.Active = isActive);
        await _context.SaveChangesAsync();
    }

    public async Task<RequestedPromo> GetPromo(int promoId)
    {
        var promo = await _context.PromoCodes
            .Include(x => x.Employee)
                .ThenInclude(x => x.User)
            .AsNoTracking()
            .FirstAsync(x => x.Id == promoId);

        return PromoMapper.MapToBl(promo);
    }

    public async Task Remove(IReadOnlyList<int> promoIds)
    {
        var promos = await _context.PromoCodes
            .Where(x => promoIds.Contains(x.Id))
            .ToListAsync();

        _context.RemoveRange(promos);
        await _context.SaveChangesAsync();
    }
}
