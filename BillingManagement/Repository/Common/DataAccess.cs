using System.Data;
using System.Data.SqlClient;

namespace BillingManagement.Repository.Common;

public class DataAccess : IDataAccess
{
    private readonly string _connectionString;

    public DataAccess(IConfiguration configuration)
    {
        _connectionString = configuration?.GetConnectionString("Hosted") ?? string.Empty;
    }

    public DataTable ExecuteQuery(string storedProcedureName, SqlParameter[] parameters = null)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();
        SqlCommand command = new(storedProcedureName, connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        if (parameters != null)
        {
            foreach (SqlParameter parameter in parameters)
            {
                command.Parameters.Add(parameter);
            }
        }

        SqlDataAdapter adapter = new(command);
        DataTable dataTable = new();
        adapter.Fill(dataTable);
        return dataTable;
    }

    public int ExecuteNonQuery(string storedProcedureName, SqlParameter[] parameters = null)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();
        SqlCommand command = new(storedProcedureName, connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        if (parameters != null)
        {
            foreach (SqlParameter parameter in parameters)
            {
                command.Parameters.Add(parameter);
            }
        }

        int result = command.ExecuteNonQuery();
        return result;
    }

    public object ExecuteScalar(string storedProcedureName, SqlParameter[] parameters = null)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();
        SqlCommand command = new(storedProcedureName, connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        if (parameters != null)
        {
            foreach (SqlParameter parameter in parameters)
            {
                command.Parameters.Add(parameter);
            }
        }

        object result = command.ExecuteScalar();
        return result;
    }
}

