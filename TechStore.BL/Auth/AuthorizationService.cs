using TechStore.BL.Exceptions;
using TechStore.BL.Mapping;
using TechStore.BL.Models;
using TechStore.DAL.Exceptions;
using TechStore.DAL.Repositories;

namespace TechStore.BL.Auth;
public class AuthorizationService
{
    private readonly AuthorizationRepository _authRepository;

    public AuthorizationService(AuthorizationRepository authRepository)
    {
        _authRepository = authRepository;
    }

    public async Task<CurrentUser> Login(string login, string password)
    {
        _ = login ?? throw new ArgumentNullException(nameof(login));
        _ = password ?? throw new ArgumentNullException(nameof(password));

        var passwordHash = PasswordGenerator.Generate(password);

        try
        {
            var user = await _authRepository.Login(login, passwordHash);
            return new(user.Login, UserTypeMapper.Map(user.UserType));
        }
        catch (UserNotFoundException e)
        {
            throw new AuthorizeException();
        }
        catch (Exception)
        {
            throw;
        }
    }
}
