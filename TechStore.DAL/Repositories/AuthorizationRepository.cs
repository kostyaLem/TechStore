using Microsoft.EntityFrameworkCore;
using TechStore.DAL.Context;
using TechStore.DAL.Exceptions;
using TechStore.DAL.Repositories.Interfaces;
using TechStore.DAL.Repositories.Models;

namespace TechStore.DAL.Repositories;

internal class AuthorizationRepository : IAuthorizationRepository
{
    private readonly TechStoreContextFactory _dbContextFactory;

    public AuthorizationRepository(TechStoreContextFactory dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<UserInfo> Login(string login, string passwordHash)
    {
        using var context = _dbContextFactory.CreateDbContext();

        var user = await context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Login == login && x.PasswordHash == passwordHash);

        if (user is null)
        {
            throw new UserNotFoundException();
        }

        if (user.PasswordHash != passwordHash)
        {
            throw new CredentionalExcetpion();
        }

        return new(user.Id, user.Login, user.Type);
    }
}
