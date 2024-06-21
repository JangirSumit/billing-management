using BillingManagement.Contracts.Abstrations;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace BillingManagement.DB.SqlServer;

public class DataAccess : IDataAccess
{
    private readonly string _connectionString;

    public DataAccess(string connectionString)
    {
        _connectionString = connectionString;
    }

    public DataTable ExecuteQuery(string query, DbParameter[] parameters = null)
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = InitializeCommand(connection, query, parameters);

        DataTable dataTable = new();
        using var adapter = new SqlDataAdapter(command);
        adapter.Fill(dataTable);

        return dataTable;
    }

    public int ExecuteNonQuery(string query, DbParameter[] parameters = null)
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = InitializeCommand(connection, query, parameters);

        connection.Open();
        return command.ExecuteNonQuery();
    }

    public object ExecuteScalar(string query, DbParameter[] parameters = null)
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = InitializeCommand(connection, query, parameters);

        connection.Open();
        return command.ExecuteScalar();
    }

    private static SqlCommand InitializeCommand(SqlConnection connection, string query, DbParameter[] parameters)
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
