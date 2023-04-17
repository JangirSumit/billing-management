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
            new("@name", System.Data.SqlDbType.NVarChar, 200, userDetail.UserName),
            new("@password", System.Data.SqlDbType.NVarChar, 500, userDetail.Password)
        });
    }

    public UserDetail GetUserByName(string userName)
    {
        var result = _dataAccess.ExecuteQuery("[dbo].[GetUserByName]", new SqlParameter[] {
             new("@name", System.Data.SqlDbType.NVarChar, 200, userName)
        });

        if (result?.Rows?.Count > 0)
        {
            return new UserDetail(result.Rows[0]["Name"].ToString(), result.Rows[0]["Password"].ToString());
        }

        return UserDetail.Empty;
    }
}
