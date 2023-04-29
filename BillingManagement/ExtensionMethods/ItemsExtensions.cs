using BillingManagement.Models;
using BillingManagement.Models.Dto;

namespace BillingManagement.ExtensionMethods;

public static class ItemsExtensions
{
    public static ItemDetail Map(this ItemDto item)
    {
        return new ItemDetail(item.Id, item.Name, item.Description, item.Quantity, item.Unit, item.RateRange1, item.RateRange2, item.Sgst, item.Cgst);
    }

    public static ItemDto Map(this ItemDetail item)
    {
        return new ItemDto(item.Id, item.Name, item.Description, item.Quantity, item.Unit, item.RateRange1, item.RateRange2, item.Sgst, item.Cgst);
    }

    public static List<ItemDto> Map(this List<ItemDetail> items)
    {
        if (items is null)
        {
            return default;
        }

        List<ItemDto> list = new();

        foreach (var item in items)
        {
            list.Add(item.Map());
        }

        return list;
    }

    public static List<ItemDetail> Map(this List<ItemDto> items)
    {
        if (items is null)
        {
            return default;
        }

        List<ItemDetail> list = new();

        foreach (var item in items)
        {
            list.Add(item.Map());
        }

        return list;
    }
}
