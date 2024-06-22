using System.Data;
using System.Data.Common;

namespace BillingManagement.Contracts.Abstrations;

public interface IDataAccess
{
    Task<DataTable> ExecuteQuery(string query, DbParameter[]? parameters = null);
    Task<int> ExecuteNonQuery(string query, DbParameter[]? parameters = null);
    Task<object> ExecuteScalar(string query, DbParameter[]? parameters = null);
}
