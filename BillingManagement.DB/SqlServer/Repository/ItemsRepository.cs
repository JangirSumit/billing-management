using BillingManagement.Contracts.Abstrations;
using BillingManagement.Contracts.Enums;
using BillingManagement.Contracts.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BillingManagement.DB.SqlServer.Repository;

public class ItemsRepository : IItemsRepository
{
    private readonly IDataAccess _dataAccess;

    public ItemsRepository(IDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }

    public async Task<bool> Add(ItemDetail item)
    {
        return await _dataAccess.ExecuteNonQuery("[dbo].[AddItem]", new SqlParameter[] {
            new("@name", item.Name),
            new("@description", item.Description),
            new("@unit", item.Unit),
            new("@rateRange1", item.RateRange1),
            new("@rateRange2", item.RateRange2),
            new("@sgst", item.Sgst),
            new("@cgst", item.Cgst)
        }) > 0;
    }

    public async Task<bool> Delete(Guid id)
    {
        return await _dataAccess.ExecuteNonQuery("[dbo].[DeleteItem]", new SqlParameter[] {
            new("@id", id)
        }) > 0;
    }

    public async Task<List<ItemDetail>> Get()
    {
        var result = new List<ItemDetail>();

        var dt = await _dataAccess.ExecuteQuery("[dbo].[GetItems]");

        if (dt == null)
            return result;

        foreach (DataRow row in dt.Rows)
        {
            result.Add(GetItem(row));
        }

        return result;
    }

    public async Task<ItemDetail> GetById(Guid id)
    {
        var dt = await _dataAccess.ExecuteQuery("[dbo].[GetItemById]", new SqlParameter[] {
            new("@id", id)
        });

        return dt.Rows.Count > 0 ? GetItem(dt.Rows[0]) : default;
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
}
