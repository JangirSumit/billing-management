using BillingManagement.Models;
using BillingManagement.Repository.Abstrations;
using System.Data;
using System.Data.SqlClient;

namespace BillingManagement.Repository;

public class VendorsRepository : IVendorsRepository
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public VendorsRepository(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("Hosted");
    }

    public VendorDetail Add(VendorDetail vendorDetail)
    {
        using (SqlConnection con = new(_connectionString))
        {
            SqlCommand command = new()
            {
                Connection = con,
                CommandText = "[dbo].[AddVendor]",
                CommandType = CommandType.StoredProcedure
            };

            SqlParameter[] parameters =
            {
                new()
                {
                    ParameterName = "@Name",
                    SqlDbType = SqlDbType.UniqueIdentifier,
                    Direction = ParameterDirection.Input,
                    Value = vendorDetail.Name
                },
                new()
                {
                    ParameterName = "@Address",
                    SqlDbType = SqlDbType.UniqueIdentifier,
                    Direction = ParameterDirection.Input,
                    Value = vendorDetail.Address
                },
                new()
                {
                    ParameterName = "@GstNumber",
                    SqlDbType = SqlDbType.UniqueIdentifier,
                    Direction = ParameterDirection.Input,
                    Value = vendorDetail.GstNumber
                }
            };

            command.Parameters.AddRange(parameters);

            con.Open();

            if (command.ExecuteNonQuery() > 0)
            {
                return new VendorDetail(Guid.NewGuid(), vendorDetail.Name, vendorDetail.Address, vendorDetail.Address);
            }
        }

        return vendorDetail;
    }

    public List<VendorDetail> GetAll()
    {
        List<VendorDetail> vendorDetails = new();

        using (SqlConnection con = new(_connectionString))
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
        VendorDetail vendorDetail = VendorDetail.Empty;

        using (SqlConnection con = new(_connectionString))
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

    public bool Delete(Guid id)
    {
        using SqlConnection con = new(_connectionString);
        SqlCommand command = new()
        {
            Connection = con,
            CommandText = "[dbo].[DeleteVendor]",
            CommandType = CommandType.StoredProcedure
        };

        SqlParameter parameter = new()
        {
            ParameterName = "@Id",
            SqlDbType = SqlDbType.UniqueIdentifier,
            Direction = ParameterDirection.Input,
            Value = id
        };

        command.Parameters.Add(parameter);

        con.Open();

        return command.ExecuteNonQuery() > 0;
    }
}
