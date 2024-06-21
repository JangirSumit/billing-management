using BillingManagement.Contracts.Models;

namespace BillingManagement.Contracts.Abstrations;

public interface IVendorsRepository
{
    void GetByGstNumber(string gstNumber);
    bool Add(VendorDetail vendorDto);
    VendorDetail GetById(Guid vendorId);
    List<VendorDetail> GetAll();
    bool Delete(Guid id);
}
