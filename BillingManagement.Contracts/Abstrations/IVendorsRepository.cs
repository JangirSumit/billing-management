using BillingManagement.Contracts.Models;

namespace BillingManagement.Contracts.Abstrations;

public interface IVendorsRepository
{
    Task GetByGstNumber(string gstNumber);
    Task<bool> Add(VendorDetail vendorDto);
    Task<VendorDetail> GetById(Guid vendorId);
    Task<List<VendorDetail>> GetAll();
    Task<bool> Delete(Guid id);
}
