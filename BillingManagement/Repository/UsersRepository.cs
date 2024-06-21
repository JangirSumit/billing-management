using BillingManagement.DB;
using BillingManagement.Models;
using BillingManagement.Repository.Abstrations;
using System.Data.SqlClient;

namespace BillingManagement.Repository;

public class UsersRepository : IUsersRepository
{
    private readonly IDataAccess _dataAccess;

    public UsersRepository(IDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }

    public int Add(UserDetail userDetail)
    {
        return _dataAccess.ExecuteNonQuery("[dbo].[AddUser]", new SqlParameter[] {
            new("@name", userDetail.UserName),
            new("@password", userDetail.Password)
        });
    }

    public int ChangePassword(string userName, string newPassword)
    {
        return _dataAccess.ExecuteNonQuery("[dbo].[ChangePassword]", new SqlParameter[] {
            new("@name", userName),
            new("@newPassword", newPassword)
        });
    }

    public UserDetail GetUserByName(string userName)
    {
        var result = _dataAccess.ExecuteQuery("[dbo].[GetUserByName]", new SqlParameter[] {
             new("@name", userName)
        });

        if (result?.Rows?.Count > 0)
        {
            return new UserDetail(result.Rows[0]["Name"].ToString(), result.Rows[0]["Password"].ToString());
        }

        return UserDetail.Empty;
    }
}
