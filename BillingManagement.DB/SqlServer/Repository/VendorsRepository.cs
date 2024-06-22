using BillingManagement.Contracts.Abstrations;
using BillingManagement.Contracts.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BillingManagement.DB.SqlServer.Repository;

public class VendorsRepository : IVendorsRepository
{
    private readonly IDataAccess _dataAccess;

    public VendorsRepository(IDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }

    public async Task<bool> Add(VendorDetail vendorDetail)
    {
        return await _dataAccess.ExecuteNonQuery("[dbo].[AddVendor]", new SqlParameter[] {
            new("@name", vendorDetail.Name),
                new("@Address",vendorDetail.Address),
                new("@GstNumber", vendorDetail.GstNumber)
        }) > 0;
    }

    public async Task<List<VendorDetail>> GetAll()
    {
        List<VendorDetail> vendorDetails = new();

        var dt = await _dataAccess.ExecuteQuery("[dbo].[GetVendors]");

        if (dt == null)
            return vendorDetails;

        foreach (DataRow row in dt.Rows)
        {
            vendorDetails.Add(GetVendor(row));
        }

        return vendorDetails;
    }

    public Task GetByGstNumber(string gstNumber)
    {
        throw new NotImplementedException();
    }

    public async Task<VendorDetail> GetById(Guid vendorId)
    {
        VendorDetail vendorDetail = VendorDetail.Empty;

        var dt = await _dataAccess.ExecuteQuery("[dbo].[GetVendors]");

        if (dt == null)
            return vendorDetail;

        if (dt.Rows.Count > 0)
        {
            vendorDetail = GetVendor(dt.Rows[0]);
        }

        return vendorDetail;
    }

    public async Task<bool> Delete(Guid id)
    {
        return await _dataAccess.ExecuteNonQuery("[dbo].[DeleteVendor]", new SqlParameter[] {
            new("@Id", id)
        }) > 0;
    }

    private VendorDetail GetVendor(DataRow row)
    {
        return new VendorDetail(Guid.Parse(Convert.ToString(row["Id"])),
                                                Convert.ToString(row["Name"]),
                                                Convert.ToString(row["Address"]),
                                                Convert.ToString(row["GstNumber"])
                                                );
    }
}
