using BillingManagement.Models;

namespace BillingManagement.Repository.Abstrations;

public interface IVendorsRepository
{
    void GetByGstNumber(string gstNumber);
    bool Add(VendorDetail vendorDto);
    VendorDetail GetById(Guid vendorId);
    List<VendorDetail> GetAll();
    bool Delete(Guid id);
}
