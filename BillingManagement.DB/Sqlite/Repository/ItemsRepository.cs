using BillingManagement.Contracts.Abstrations;
using BillingManagement.Contracts.Enums;
using BillingManagement.Contracts.Models;
using Microsoft.Data.Sqlite;
using System.Data;

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
        string query = @"
                INSERT INTO Items (Name, Description, Unit, RateRange1, RateRange2, Sgst, Cgst)
                VALUES (@name, @description, @unit, @rateRange1, @rateRange2, @sgst, @cgst)";

        return _dataAccess.ExecuteNonQuery(query, new SqliteParameter[]
        {
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
        string query = "DELETE FROM Items WHERE Id = @id";

        return _dataAccess.ExecuteNonQuery(query, new SqliteParameter[]
        {
            new("@id", id)
        }) > 0;
    }

    public List<ItemDetail> Get()
    {
        var result = new List<ItemDetail>();

        string query = "SELECT * FROM Items";
        var dt = _dataAccess.ExecuteQuery(query);

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
                              Convert.ToDouble(row["Cgst"]));
    }

    public ItemDetail GetById(Guid id)
    {
        string query = "SELECT * FROM Items WHERE Id = @id";

        var dt = _dataAccess.ExecuteQuery(query, new SqliteParameter[]
        {
            new("@id", id)
        });

        return dt.Rows.Count > 0 ? GetItem(dt.Rows[0]) : default;
    }
}
