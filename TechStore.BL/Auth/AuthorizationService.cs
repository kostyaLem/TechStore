using DevExpress.Mvvm.POCO;
using TechStore.BL.Exceptions;
using TechStore.BL.Models;
using TechStore.DAL.Exceptions;
using TechStore.DAL.Repositories.Interfaces;

namespace TechStore.BL.Auth;

internal class AuthorizationService : IAuthorizationService
{
    private readonly IAuthorizationRepository _authRepository;

    public AuthorizationService(IAuthorizationRepository authRepository)
    {
        _authRepository = authRepository;
    }

    public async Task<CurrentUser> Login(string login, string password)
    {
        ArgumentNullException.ThrowIfNull(nameof(login));
        ArgumentNullException.ThrowIfNull(nameof(password));

        var passwordHash = HashService.Compute(password);

        try
        {
            var user = await _authRepository.Login(login, passwordHash);
            return new(user.Id, user.Login, UserType.Employee);
        }
        catch (UserNotFoundException)
        {
            throw new UserNotFoundAuthorizeException();
        }
        catch (CredentionalExcetpion)
        {
            throw new AuthorizeException();
        }
        catch (Exception)
        {
            throw;
        }
    }
}
