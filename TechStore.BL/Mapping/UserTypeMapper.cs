using TechStore.Domain.Models;
using DomainUserType = TechStore.Domain.Models.UserType;

namespace TechStore.BL.Mapping;

public static class UserTypeMapper
{
    public static UserType Map(DomainUserType userType)
        => userType switch
        {
            DomainUserType.Admin => UserType.Admin,
            DomainUserType.Employee => UserType.Employee,
            _ => throw new Exception($"Unsupported user type {userType}.")
        };
}
