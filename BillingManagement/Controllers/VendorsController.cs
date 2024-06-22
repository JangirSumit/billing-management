using BillingManagement.Contracts.Abstrations;
using BillingManagement.Dto;
using BillingManagement.ExtensionMethods;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BillingManagement.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class VendorsController : ControllerBase
{
    private readonly IVendorsRepository _vendorsRepository;

    public VendorsController(IVendorsRepository vendorsRepository)
    {
        _vendorsRepository = vendorsRepository;
    }

    [HttpGet]
    public async Task<List<VendorDto>> Get()
    {
        return (await _vendorsRepository.GetAll()).Map();
    }

    [HttpGet("{id}")]
    public async Task<VendorDto> Get(Guid id)
    {
        return (await _vendorsRepository.GetById(id)).Map();
    }

    [HttpPost]
    public async Task<bool> Post([FromBody] VendorDto vendorDto)
    {
        var vendorDetail = vendorDto.Map();
        return await _vendorsRepository.Add(vendorDetail);
    }

    [HttpDelete("{id}")]
    public async Task<bool> Delete(Guid id)
    {
        return await _vendorsRepository.Delete(id);
    }
}
