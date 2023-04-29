using BillingManagement.Enums;
using BillingManagement.Models;
using BillingManagement.Repository.Abstrations;
using BillingManagement.Repository.Common;
using System.Data;
using System.Data.SqlClient;

namespace BillingManagement.Repository;

public class ItemsRepository : IItemsRepository
{
    private readonly IDataAccess _dataAccess;

    public ItemsRepository(IDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }

    public bool Add(ItemDetail item)
    {
        return _dataAccess.ExecuteNonQuery("[dbo].[AddItem]", new SqlParameter[] {
            new("@name", item.Name),
            new("@description", item.Description),
            new("@unit", item.Unit),
            new("@rateRange1", item.RateRange1),
            new("@rateRange2", item.RateRange2),
            new("@sgst", item.Sgst),
            new("@cgst", item.Cgst)
        }) > 0;
    }

    public bool Delete(Guid id)
    {
        return _dataAccess.ExecuteNonQuery("[dbo].[DeleteItem]", new SqlParameter[] {
            new("@id", id)
        }) > 0;
    }

    public List<ItemDetail> Get()
    {
        var result = new List<ItemDetail>();

        var dt = _dataAccess.ExecuteQuery("[dbo].[getItems]");

        if (dt == null)
            return result;

        foreach (DataRow row in dt.Rows)
        {
            var item = new ItemDetail(Guid.Parse(Convert.ToString(row["Id"])),
                                        Convert.ToString(row["Name"]),
                                        Convert.ToString(row["Description"]),
                                        Convert.ToInt32(row["Quantity"]),
                                        (ItemUnit)Enum.Parse(typeof(ItemUnit), Convert.ToString(row["Unit"])),
                                        Convert.ToDouble(row["RateRange1"]),
                                        Convert.ToDouble(row["RateRange2"]),
                                        Convert.ToDouble(row["Sgst"]),
                                        Convert.ToDouble(row["Cgst"])
                                        );
            result.Add(item);
        }

        return result;
    }

    public ItemDetail GetById(Guid id)
    {
        throw new NotImplementedException();
    }
}
