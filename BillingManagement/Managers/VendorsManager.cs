using BillingManagement.Abstrations;
using BillingManagement.Models;
using System.Data.SqlClient;

namespace BillingManagement.Managers;

public class VendorsManager : IVendorsManager
{
    private readonly IConfiguration _configuration;

    public VendorsManager()
    {
        _configuration = new ConfigurationManager();
    }

    public Guid Add(VendorDetail vendorDto)
    {
        throw new NotImplementedException();
    }

    public List<VendorDetail> GetAll()
    {
        var connectionString = _configuration.GetConnectionString("Hosted");

        using (SqlConnection con = new(connectionString))
        {

        }

        return new List<VendorDetail>();
    }

    public void GetByGstNumber(string gstNumber)
    {
        throw new NotImplementedException();
    }

    public VendorDetail GetById(Guid vendorId)
    {
        throw new NotImplementedException();
    }
}
