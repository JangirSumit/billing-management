using BillingManagement.Models.Dto;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BillingManagement.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VendorsController : ControllerBase
{
    public VendorsController()
    {
    }

    [HttpGet]
    public List<VendorDto> Get()
    {
        return new List<VendorDto>() {
            new VendorDto(Guid.NewGuid(), "Dummy", "Description", "ABC-DEF-GHI-JKL")
        };
    }

    [HttpGet("{id}")]
    public VendorDto Get(Guid id)
    {
        return new VendorDto(Guid.NewGuid(), "Dummy", "Description", "ABC-DEF-GHI-JKL");
    }

    [HttpPost]
    public VendorDto Post([FromBody] VendorDto vendorDto)
    {
        var result = new VendorDto(Guid.NewGuid(), vendorDto.Name, vendorDto.Address, vendorDto.GstNumber);
        return result;
    }

    [HttpDelete("{id}")]
    public bool Delete(Guid id)
    {
        return true;
    }
}
