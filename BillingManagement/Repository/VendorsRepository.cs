using BillingManagement.DB;
using BillingManagement.Models;
using BillingManagement.Repository.Abstrations;
using System.Data;
using System.Data.SqlClient;

namespace BillingManagement.Repository;

public class VendorsRepository : IVendorsRepository
{
    private readonly IDataAccess _dataAccess;

    public VendorsRepository(IDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }

    public bool Add(VendorDetail vendorDetail)
    {
        return _dataAccess.ExecuteNonQuery("[dbo].[AddVendor]", new SqlParameter[] {
            new("@name", vendorDetail.Name),
                new("@Address",vendorDetail.Address),
                new("@GstNumber", vendorDetail.GstNumber)
        }) > 0;
    }

    public List<VendorDetail> GetAll()
    {
        List<VendorDetail> vendorDetails = new();

        var dt = _dataAccess.ExecuteQuery("[dbo].[GetVendors]");

        if (dt == null)
            return vendorDetails;

        foreach (DataRow row in dt.Rows)
        {
            vendorDetails.Add(GetVendor(row));
        }

        return vendorDetails;
    }

    public VendorDetail GetVendor(DataRow row)
    {
        return new VendorDetail(Guid.Parse(Convert.ToString(row["Id"])),
                                                Convert.ToString(row["Name"]),
                                                Convert.ToString(row["Address"]),
                                                Convert.ToString(row["GstNumber"])
                                                );
    }

    public void GetByGstNumber(string gstNumber)
    {
        throw new NotImplementedException();
    }

    public VendorDetail GetById(Guid vendorId)
    {
        VendorDetail vendorDetail = VendorDetail.Empty;

        var dt = _dataAccess.ExecuteQuery("[dbo].[GetVendors]");

        if (dt == null)
            return vendorDetail;

        if (dt.Rows.Count > 0)
        {
            vendorDetail = GetVendor(dt.Rows[0]);
        }

        return vendorDetail;
    }

    public bool Delete(Guid id)
    {
        return _dataAccess.ExecuteNonQuery("[dbo].[DeleteVendor]", new SqlParameter[] {
            new("@Id", id)
        }) > 0;
    }
}
