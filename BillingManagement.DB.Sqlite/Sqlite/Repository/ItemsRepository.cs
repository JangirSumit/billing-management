using System.Data;
using Microsoft.Data.SqlClient;
using BillingManagement.Contracts.Abstrations;
using BillingManagement.Contracts.Models;
using BillingManagement.Contracts.Enums;

namespace BillingManagement.DB.Sqlite.Repository;

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

        var dt = _dataAccess.ExecuteQuery("[dbo].[GetItems]");

        if (dt == null)
            return result;

        foreach (DataRow row in dt.Rows)
        {
            result.Add(GetItem(row));
        }

        return result;
    }

    private static ItemDetail GetItem(DataRow row)
    {
        return new ItemDetail(Guid.Parse(Convert.ToString(row["Id"])),
                                                Convert.ToString(row["Name"]),
                                                Convert.ToString(row["Description"]),
                                                (ItemUnit)Convert.ToInt32(row["Unit"]),
                                                Convert.ToDouble(row["RateRange1"]),
                                                Convert.ToDouble(row["RateRange2"]),
                                                Convert.ToDouble(row["Sgst"]),
                                                Convert.ToDouble(row["Cgst"])
                                                );
    }

    public ItemDetail GetById(Guid id)
    {
        var dt = _dataAccess.ExecuteQuery("[dbo].[GetItemById]", new SqlParameter[] {
            new("@id", id)
        });

        return dt.Rows.Count > 0 ? GetItem(dt.Rows[0]) : default;
    }
}
