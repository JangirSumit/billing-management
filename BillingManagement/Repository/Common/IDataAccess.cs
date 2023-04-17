using System.Data;
using System.Data.SqlClient;

namespace BillingManagement.Repository.Common;

public interface IDataAccess
{
    DataTable ExecuteQuery(string storedProcedureName, SqlParameter[] parameters = null);
    int ExecuteNonQuery(string storedProcedureName, SqlParameter[] parameters = null);
    object ExecuteScalar(string storedProcedureName, SqlParameter[] parameters = null);
}
