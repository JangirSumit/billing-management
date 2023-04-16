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
    private readonly IVendorsRepository _vendorsManager;

    public VendorsController(IVendorsRepository vendorsManager)
    {
        _vendorsManager = vendorsManager;
    }

    [HttpGet]
    public List<VendorDto> Get()
    {
        return _vendorsManager.GetAll().Map();
    }

    [HttpGet("{id}")]
    public VendorDto Get(Guid id)
    {
        return _vendorsManager.GetById(id).Map();
    }

    [HttpPost]
    public VendorDto Post([FromBody] VendorDto vendorDto)
    {
        var vendorDetail = vendorDto.Map();
        vendorDetail = _vendorsManager.Add(vendorDetail);
        return vendorDetail.Map();
    }

    [HttpDelete("{id}")]
    public bool Delete(Guid id)
    {
        return _vendorsManager.Delete(id);
    }
}
