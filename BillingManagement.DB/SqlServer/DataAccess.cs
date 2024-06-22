using BillingManagement.Contracts.Abstrations;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.Common;

namespace BillingManagement.DB.SqlServer;

public class DataAccess : IDataAccess
{
    private readonly string _connectionString;

    public DataAccess(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Hosted") ?? throw new NotImplementedException("Connection String not found...");
    }

    public async Task<DataTable> ExecuteQuery(string query, DbParameter[]? parameters = null)
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = InitializeCommand(connection, query, parameters);

        return await Task.Run(() =>
        {
            DataTable dataTable = new();
            using var adapter = new SqlDataAdapter(command);
            adapter.Fill(dataTable);

            return dataTable;
        });
    }

    public async Task<int> ExecuteNonQuery(string query, DbParameter[]? parameters = null)
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = InitializeCommand(connection, query, parameters);

        connection.Open();

        return await command.ExecuteNonQueryAsync();
    }

    public async Task<object> ExecuteScalar(string query, DbParameter[]? parameters = null)
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = InitializeCommand(connection, query, parameters);

        connection.Open();

        return await command.ExecuteScalarAsync();
    }

    private static SqlCommand InitializeCommand(SqlConnection connection, string query, DbParameter[]? parameters)
    {
        var command = new SqlCommand(query, connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        if (parameters != null)
        {
            foreach (var parameter in parameters)
            {
                command.Parameters.Add(parameter);
            }
        }

        return command;
    }
}
