using BillingManagement.Contracts.Abstrations;
using BillingManagement.Contracts.Models;
using Microsoft.Data.SqlClient;

namespace BillingManagement.DB.SqlServer.Repository;

public class UsersRepository : IUsersRepository
{
    private readonly IDataAccess _dataAccess;

    public UsersRepository(IDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }

    public async Task<int> Add(UserDetail userDetail)
    {
        return await _dataAccess.ExecuteNonQuery("[dbo].[AddUser]", new SqlParameter[] {
            new("@name", userDetail.UserName),
            new("@password", userDetail.Password)
        });
    }

    public async Task<int> ChangePassword(string userName, string newPassword)
    {
        return await _dataAccess.ExecuteNonQuery("[dbo].[ChangePassword]", new SqlParameter[] {
            new("@name", userName),
            new("@newPassword", newPassword)
        });
    }

    public async Task<UserDetail> GetUserByName(string userName)
    {
        var result = await _dataAccess.ExecuteQuery("[dbo].[GetUserByName]", new SqlParameter[] {
             new("@name", userName)
        });

        if (result?.Rows?.Count > 0)
        {
            return new UserDetail(result.Rows[0]["Name"].ToString(), result.Rows[0]["Password"].ToString());
        }

        return UserDetail.Empty;
    }
}
