using BillingManagement.ExtensionMethods;
using BillingManagement.Models.Dto;
using BillingManagement.Repository.Abstrations;
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
    public IEnumerable<ItemDto> Get()
    {
        return _itemsRepository.Get().Map();
    }

    [HttpGet("{id}")]
    public ItemDto Get(Guid id)
    {
        return _itemsRepository.GetById(id).Map();
    }

    [HttpPost]
    public bool Post([FromBody] ItemDto item)
    {
        return _itemsRepository.Add(item.Map());
    }

    [HttpDelete("{id}")]
    public bool Delete(Guid id)
    {
        return _itemsRepository.Delete(id);
    }
}
