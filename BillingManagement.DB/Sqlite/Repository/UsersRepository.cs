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

    public async Task<int> Add(UserDetail userDetail)
    {
        var query = @"
                INSERT INTO Users (Name, Password)
                VALUES (@name, @password)";

        return await Task.Run(() =>
            _dataAccess.ExecuteNonQuery(query, new SqliteParameter[]
            {
                new("@name", userDetail.UserName),
                new("@password", userDetail.Password)
            })
        );
    }

    public async Task<int> ChangePassword(string userName, string newPassword)
    {
        var query = @"
                UPDATE Users
                SET Password = @newPassword
                WHERE Name = @name";

        return await Task.Run(() =>
            _dataAccess.ExecuteNonQuery(query, new SqliteParameter[]
            {
                new("@name", userName),
                new("@newPassword", newPassword)
            })
        );
    }

    public async Task<UserDetail> GetUserByName(string userName)
    {
        var query = "SELECT * FROM Users WHERE Name = @name";

        var result = await Task.Run(() =>
            _dataAccess.ExecuteQuery(query, new SqliteParameter[]
            {
                new("@name", userName)
            })
        );

        if (result?.Rows?.Count > 0)
        {
            return new UserDetail(result.Rows[0]["Name"].ToString(), result.Rows[0]["Password"].ToString());
        }

        return UserDetail.Empty;
    }
}
