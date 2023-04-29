using BillingManagement.Models;
using BillingManagement.Models.Dto;

namespace BillingManagement.Repository.Abstrations;

public interface IItemsRepository
{
    List<ItemDetail> Get();
    ItemDetail GetById(Guid id);
    bool Add(ItemDetail item);
    bool Delete(Guid id);
}
