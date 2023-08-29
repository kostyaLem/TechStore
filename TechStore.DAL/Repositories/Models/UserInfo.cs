using TechStore.Domain.Models;

namespace TechStore.DAL.Repositories.Models;

public record UserInfo(int Id, string Login, UserType UserType);
