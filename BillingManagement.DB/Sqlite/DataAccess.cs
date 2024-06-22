using BillingManagement.Contracts.Abstrations;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.Common;

namespace BillingManagement.DB.Sqlite;

public class DataAccess : IDataAccess
{
    private readonly string _connectionString;

    public DataAccess(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Hosted") ?? throw new NotImplementedException("Connection String not found...");
    }

    public async Task<DataTable> ExecuteQuery(string query, DbParameter[] parameters = null)
    {
        using var connection = new SqliteConnection(_connectionString);
        using var command = InitializeCommand(connection, query, parameters);

        DataTable dataTable = new();

        connection.Open();
        using var reader = await command.ExecuteReaderAsync();
        dataTable.Load(reader);

        return dataTable;
    }

    public async Task<int> ExecuteNonQuery(string query, DbParameter[] parameters = null)
    {
        using var connection = new SqliteConnection(_connectionString);
        using var command = InitializeCommand(connection, query, parameters);

        connection.Open();
        return await command.ExecuteNonQueryAsync();
    }

    public async Task<object> ExecuteScalar(string query, DbParameter[] parameters = null)
    {
        using var connection = new SqliteConnection(_connectionString);
        using var command = InitializeCommand(connection, query, parameters);

        connection.Open();
        return await command.ExecuteScalarAsync();
    }

    private static SqliteCommand InitializeCommand(SqliteConnection connection, string query, DbParameter[] parameters)
    {
        var command = new SqliteCommand(query, connection)
        {
            CommandType = CommandType.Text
        };

        if (parameters != null)
        {
            foreach (DbParameter parameter in parameters)
            {
                command.Parameters.Add(parameter);
            }
        }

        return command;
    }
}
