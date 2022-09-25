using TechStore.DAL.Repositories.Models;

namespace TechStore.DAL.Repositories.Interfaces;

public interface IAuthorizationRepository
{
    Task<UserInfo> Login(string login, string passwordHash);
}