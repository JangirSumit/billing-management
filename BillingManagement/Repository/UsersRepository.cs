using BillingManagement.Models;
using BillingManagement.Repository.Abstrations;
using BillingManagement.Repository.Common;
using System.Data.SqlClient;

namespace BillingManagement.Repository;

public class UsersRepository : IUsersRepository
{
    private readonly IConfiguration _configuration;
    private readonly IDataAccess _dataAccess;

    public UsersRepository(IConfiguration configuration, IDataAccess dataAccess)
    {
        _configuration = configuration; ;
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
