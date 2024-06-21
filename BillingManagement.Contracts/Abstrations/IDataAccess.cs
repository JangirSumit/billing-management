using System.Data;
using System.Data.Common;

namespace BillingManagement.Contracts.Abstrations;

public interface IDataAccess
{
    DataTable ExecuteQuery(string query, DbParameter[] parameters = null);
    int ExecuteNonQuery(string query, DbParameter[] parameters = null);
    object ExecuteScalar(string query, DbParameter[] parameters = null);
}
