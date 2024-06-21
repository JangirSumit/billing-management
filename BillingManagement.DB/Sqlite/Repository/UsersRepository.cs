using BillingManagement.Contracts.Abstrations;
using BillingManagement.Contracts.Models;
using Microsoft.Data.Sqlite;

namespace BillingManagement.DB.Sqlite.Repository;

public class UsersRepository : IUsersRepository
{
    private readonly IDataAccess _dataAccess;

    public UsersRepository(IDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }

    public int Add(UserDetail userDetail)
    {
        string query = @"
                INSERT INTO Users (Name, Password)
                VALUES (@name, @password)";

        return _dataAccess.ExecuteNonQuery(query, new SqliteParameter[]
        {
            new("@name", userDetail.UserName),
            new("@password", userDetail.Password)
        });
    }

    public int ChangePassword(string userName, string newPassword)
    {
        string query = @"
                UPDATE Users
                SET Password = @newPassword
                WHERE Name = @name";

        return _dataAccess.ExecuteNonQuery(query, new SqliteParameter[]
        {
            new("@name", userName),
            new("@newPassword", newPassword)
        });
    }

    public UserDetail GetUserByName(string userName)
    {
        string query = "SELECT * FROM Users WHERE Name = @name";

        var result = _dataAccess.ExecuteQuery(query, new SqliteParameter[]
        {
            new("@name", userName)
        });

        if (result?.Rows?.Count > 0)
        {
            return new UserDetail(result.Rows[0]["UserName"].ToString(), result.Rows[0]["Password"].ToString());
        }

        return UserDetail.Empty;
    }
}
