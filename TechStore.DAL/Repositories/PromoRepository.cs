using Microsoft.EntityFrameworkCore;
using TechStore.DAL.Context;
using TechStore.DAL.Mapping;
using TechStore.DAL.Repositories.Interfaces;
using TechStore.DAL.Repositories.Models.Promos;
using TechStore.Domain.Models;

namespace TechStore.DAL.Repositories;

internal sealed class PromoRepository : IPromoRepository
{
    private readonly TechStoreContextFactory _dbContextFactory;

    public PromoRepository(TechStoreContextFactory dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<IReadOnlyList<RequestedPromo>> GetPromos()
    {
        using var context = _dbContextFactory.CreateDbContext();

        var promos = await context.PromoCodes
            .Include(x => x.Employee)
            .ThenInclude(x => x.User)
            .AsNoTracking()
            .ToListAsync();

        return promos.Select(PromoMapper.MapToBl).ToList();
    }

    public async Task<IReadOnlyList<RequestedPromo>> GetActivePromos()
    {
        using var context = _dbContextFactory.CreateDbContext();

        var promos = await context.PromoCodes
            .Include(x => x.Employee)
            .ThenInclude(x => x.User)
            .Where(x => x.Active)
            .AsNoTracking()
            .ToListAsync();

        return promos.Select(PromoMapper.MapToBl).ToList();
    }

    public async Task Create(PromoDefinition promo)
    {
        using var context = _dbContextFactory.CreateDbContext();

        var employee = context.Employees
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

        await context.PromoCodes.AddAsync(newPromo);
        await context.SaveChangesAsync();
    }

    public async Task<RequestedPromo> Update(int promoId, PromoDefinition updated)
    {
        using var context = _dbContextFactory.CreateDbContext();

        var promo = await context.PromoCodes
            .Include(x => x.Employee)
                .ThenInclude(x => x.User)
            .FirstAsync(x => x.Id == promoId);

        promo.Name = updated.Name;
        promo.Discount = updated.Discount;
        promo.Active = updated.IsActive;

        await context.SaveChangesAsync();

        return PromoMapper.MapToBl(promo);
    }

    public async Task ChangeActiveStatus(IReadOnlyList<int> promoIds, bool isActive)
    {
        using var context = _dbContextFactory.CreateDbContext();

        var promos = await context.PromoCodes
            .Where(x => promoIds.Contains(x.Id))
            .ToListAsync();

        promos.ForEach(x => x.Active = isActive);
        await context.SaveChangesAsync();
    }

    public async Task<RequestedPromo> GetPromo(int promoId)
    {
        using var context = _dbContextFactory.CreateDbContext();

        var promo = await context.PromoCodes
            .Include(x => x.Employee)
                .ThenInclude(x => x.User)
            .AsNoTracking()
            .FirstAsync(x => x.Id == promoId);

        return PromoMapper.MapToBl(promo);
    }

    public async Task Remove(IReadOnlyList<int> promoIds)
    {
        using var context = _dbContextFactory.CreateDbContext();

        var promos = await context.PromoCodes
            .Where(x => promoIds.Contains(x.Id))
            .ToListAsync();

        context.RemoveRange(promos);
        await context.SaveChangesAsync();
    }
}
