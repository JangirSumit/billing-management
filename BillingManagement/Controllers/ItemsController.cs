using BillingManagement.Contracts.Abstrations;
using BillingManagement.Dto;
using BillingManagement.ExtensionMethods;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BillingManagement.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ItemsController : ControllerBase
{
    private readonly IItemsRepository _itemsRepository;

    public ItemsController(IItemsRepository itemsRepository)
    {
        _itemsRepository = itemsRepository;
    }

    [HttpGet]
    public async Task<IEnumerable<ItemDto>> Get()
    {
        return (await _itemsRepository.Get()).Map();
    }

    [HttpGet("{id}")]
    public async Task<ItemDto> Get(Guid id)
    {
        return (await _itemsRepository.GetById(id)).Map();
    }

    [HttpPost]
    public async Task<bool> Post([FromBody] ItemDto item)
    {
        return await _itemsRepository.Add(item.Map());
    }

    [HttpDelete("{id}")]
    public async Task<bool> Delete(Guid id)
    {
        return await _itemsRepository.Delete(id);
    }
}
