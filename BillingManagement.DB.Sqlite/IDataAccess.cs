using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace BillingManagement.DB;

public interface IDataAccess
{
    DataTable ExecuteQuery(string query, DbParameter[] parameters = null);
    int ExecuteNonQuery(string query, DbParameter[] parameters = null);
    object ExecuteScalar(string query, DbParameter[] parameters = null);
}
