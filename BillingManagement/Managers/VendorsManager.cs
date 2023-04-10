using BillingManagement.Abstrations;
using BillingManagement.Models;
using System.Data;
using System.Data.SqlClient;

namespace BillingManagement.Managers;

public class VendorsManager : IVendorsManager
{
    private readonly IConfiguration _configuration;

    public VendorsManager(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Guid Add(VendorDetail vendorDto)
    {
        throw new NotImplementedException();
    }

    public List<VendorDetail> GetAll()
    {
        var connectionString = _configuration.GetConnectionString("Hosted");
        List<VendorDetail> vendorDetails = new();

        using (SqlConnection con = new(connectionString))
        {
            SqlCommand command = new()
            {
                Connection = con,
                CommandText = "[dbo].[GetVendors]",
                CommandType = CommandType.StoredProcedure
            };

            con.Open();

            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    vendorDetails.Add(GetVendor(reader));
                }
            }
            else
            {
                Console.WriteLine("No rows found.");
            }
            reader.Close();
        }

        return vendorDetails;
    }

    public VendorDetail GetVendor(SqlDataReader reader)
    {
        var id = reader.GetGuid("Id");
        var name = reader.GetString("Name");
        var address = reader.GetString("Address");
        var gstNumber = reader.GetString("GstNumber");

        var vendor = new VendorDetail(id, name, address, gstNumber);

        return vendor;
    }

    public void GetByGstNumber(string gstNumber)
    {
        throw new NotImplementedException();
    }

    public VendorDetail GetById(Guid vendorId)
    {
        var connectionString = _configuration.GetConnectionString("Hosted");
        VendorDetail vendorDetail = VendorDetail.Empty;

        using (SqlConnection con = new(connectionString))
        {
            SqlCommand command = new()
            {
                Connection = con,
                CommandText = "[dbo].[GetVendor]",
                CommandType = CommandType.StoredProcedure
            };

            SqlParameter parameter = new()
            {
                ParameterName = "@Id",
                SqlDbType = SqlDbType.UniqueIdentifier,
                Direction = ParameterDirection.Input,
                Value = vendorId
            };

            command.Parameters.Add(parameter);

            con.Open();

            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    vendorDetail = GetVendor(reader);
                }
            }
            else
            {
                Console.WriteLine("No rows found.");
            }
            reader.Close();
        }

        return vendorDetail;
    }
}
