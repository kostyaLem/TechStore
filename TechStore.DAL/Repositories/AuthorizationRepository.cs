using Microsoft.EntityFrameworkCore;
using TechStore.DAL.Context;
using TechStore.DAL.Repositories.Models;

namespace TechStore.DAL.Repositories;

public class AuthorizationRepository
{
    private readonly TechStoreContext _context;

    public AuthorizationRepository(TechStoreContext context)
    {
        _context = context;
    }

    public async Task<UserInfo> Login(string login, string passwordHash)
    {
        var user = await _context.Users.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Login == login && x.PasswordHash == passwordHash);

        if (user is null)
            throw new Exception("User not authorized");

        return new(user.Login, user.Type);
    }
}
