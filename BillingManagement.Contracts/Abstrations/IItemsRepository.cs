using BillingManagement.Contracts.Models;

namespace BillingManagement.Contracts.Abstrations;

public interface IItemsRepository
{
    List<ItemDetail> Get();
    ItemDetail GetById(Guid id);
    bool Add(ItemDetail item);
    bool Delete(Guid id);
}
