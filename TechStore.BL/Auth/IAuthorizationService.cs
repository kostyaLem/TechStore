using TechStore.BL.Models;

namespace TechStore.BL.Auth;

public interface IAuthorizationService
{
    Task<CurrentUser> Login(string login, string password);
}