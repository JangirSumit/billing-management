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
    public List<VendorDto> Get()
    {
        return _vendorsRepository.GetAll().Map();
    }

    [HttpGet("{id}")]
    public VendorDto Get(Guid id)
    {
        return _vendorsRepository.GetById(id).Map();
    }

    [HttpPost]
    public bool Post([FromBody] VendorDto vendorDto)
    {
        var vendorDetail = vendorDto.Map();
        return _vendorsRepository.Add(vendorDetail);
    }

    [HttpDelete("{id}")]
    public bool Delete(Guid id)
    {
        return _vendorsRepository.Delete(id);
    }
}
