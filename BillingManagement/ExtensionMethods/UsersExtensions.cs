using BillingManagement.Models;
using BillingManagement.Models.Dto;

namespace BillingManagement.ExtensionMethods;

public static class UsersExtensions
{
    public static UserDetail Map(UserDto user)
    {
        return new UserDetail(user.UserName, user.Password);
    }

    public static UserDto Map(UserDetail user)
    {
        return new UserDto(user.UserName, user.Password);
    }
}
