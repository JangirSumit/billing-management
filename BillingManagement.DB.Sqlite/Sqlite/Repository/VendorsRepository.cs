using BillingManagement.Contracts.Abstrations;
using BillingManagement.Contracts.Models;
using Microsoft.Data.Sqlite;
using System.Data;

namespace BillingManagement.DB.Sqlite.Repository;

public class VendorsRepository : IVendorsRepository
{
    private readonly IDataAccess _dataAccess;

    public VendorsRepository(IDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }

    public bool Add(VendorDetail vendorDetail)
    {
        string query = @"
                INSERT INTO Vendors (Name, Address, GstNumber)
                VALUES (@name, @Address, @GstNumber)";

        return _dataAccess.ExecuteNonQuery(query, new SqliteParameter[]
        {
            new("@name", vendorDetail.Name),
            new("@Address", vendorDetail.Address),
            new("@GstNumber", vendorDetail.GstNumber)
        }) > 0;
    }

    public List<VendorDetail> GetAll()
    {
        List<VendorDetail> vendorDetails = new();

        string query = "SELECT * FROM Vendors";
        var dt = _dataAccess.ExecuteQuery(query);

        if (dt == null)
            return vendorDetails;

        foreach (DataRow row in dt.Rows)
        {
            vendorDetails.Add(GetVendor(row));
        }

        return vendorDetails;
    }

    public static VendorDetail GetVendor(DataRow row)
    {
        return new VendorDetail(
            Guid.Parse(Convert.ToString(row["Id"])),
            Convert.ToString(row["Name"]),
            Convert.ToString(row["Address"]),
            Convert.ToString(row["GstNumber"])
        );
    }

    public VendorDetail GetById(Guid vendorId)
    {
        VendorDetail vendorDetail = VendorDetail.Empty;

        string query = "SELECT * FROM Vendors WHERE Id = @Id";
        var dt = _dataAccess.ExecuteQuery(query, new SqliteParameter[]
        {
            new("@Id", vendorId)
        });

        if (dt == null || dt.Rows.Count == 0)
            return vendorDetail;

        return GetVendor(dt.Rows[0]);
    }

    public bool Delete(Guid id)
    {
        string query = "DELETE FROM Vendors WHERE Id = @Id";

        return _dataAccess.ExecuteNonQuery(query, new SqliteParameter[]
        {
            new("@Id", id)
        }) > 0;
    }

    // Not implemented in the original code, added method definition for consistency
    public void GetByGstNumber(string gstNumber)
    {
        throw new NotImplementedException();
    }
}
