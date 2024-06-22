using BillingManagement.Contracts.Models;

namespace BillingManagement.Contracts.Abstrations;

public interface IItemsRepository
{
    Task<List<ItemDetail>> Get();
    Task<ItemDetail> GetById(Guid id);
    Task<bool> Add(ItemDetail item);
    Task<bool> Delete(Guid id);
}
