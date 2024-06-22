using BillingManagement.Contracts.Models;

namespace BillingManagement.Contracts.Abstrations;

public interface IUsersRepository
{
    Task<int> Add(UserDetail userDetail);
    Task<int> ChangePassword(string userName, string newPassword);
    Task<UserDetail> GetUserByName(string userName);
}
