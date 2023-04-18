using BillingManagement.Models;

namespace BillingManagement.Repository.Abstrations;

public interface IUsersRepository
{
    int Add(UserDetail userDetail);
    int ChangePassword(string userName, string newPassword);
    UserDetail GetUserByName(string userName);
}
