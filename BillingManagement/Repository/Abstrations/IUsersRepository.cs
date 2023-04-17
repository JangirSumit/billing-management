using BillingManagement.Models;

namespace BillingManagement.Repository.Abstrations;

public interface IUsersRepository
{
    int Add(UserDetail userDetail);
    UserDetail GetUserByName(string userName);
}
