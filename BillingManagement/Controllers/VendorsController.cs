using BillingManagement.ExtensionMethods;
using BillingManagement.Models.Dto;
using BillingManagement.Repository.Abstrations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
    public VendorDto Post([FromBody] VendorDto vendorDto)
    {
        var vendorDetail = vendorDto.Map();
        vendorDetail = _vendorsRepository.Add(vendorDetail);
        return vendorDetail.Map();
    }

    [HttpDelete("{id}")]
    public bool Delete(Guid id)
    {
        return _vendorsRepository.Delete(id);
    }
}
