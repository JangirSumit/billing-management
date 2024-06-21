using BillingManagement.Contracts.Models;

namespace BillingManagement.Contracts.Abstrations;

public interface IUsersRepository
{
    int Add(UserDetail userDetail);
    int ChangePassword(string userName, string newPassword);
    UserDetail GetUserByName(string userName);
}
