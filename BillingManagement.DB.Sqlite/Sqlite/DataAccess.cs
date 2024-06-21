using Microsoft.Data.Sqlite;
using System.Data;
using System.Data.Common;

namespace BillingManagement.DB.Sqlite;

public class DataAccess : IDataAccess
{
    private readonly string _connectionString;

    public DataAccess(string connectionString)
    {
        _connectionString = connectionString;
    }

    public DataTable ExecuteQuery(string query, DbParameter[] parameters = null)
    {
        using var connection = new SqliteConnection(_connectionString);
        using var command = InitializeCommand(connection, query, parameters);

        DataTable dataTable = new();

        connection.Open();
        using var reader = command.ExecuteReader();
        dataTable.Load(reader);

        return dataTable;
    }

    public int ExecuteNonQuery(string query, DbParameter[] parameters = null)
    {
        using var connection = new SqliteConnection(_connectionString);
        using var command = InitializeCommand(connection, query, parameters);

        connection.Open();
        return command.ExecuteNonQuery();
    }

    public object ExecuteScalar(string query, DbParameter[] parameters = null)
    {
        using var connection = new SqliteConnection(_connectionString);
        using var command = InitializeCommand(connection, query, parameters);

        connection.Open();
        return command.ExecuteScalar();
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
