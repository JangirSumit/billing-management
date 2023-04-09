using BillingManagement.Models;

namespace BillingManagement.Abstrations
{
    public interface IVendorsManager
    {
        void GetByGstNumber(string gstNumber);
        Guid Add(VendorDetail vendorDto);
        VendorDetail GetById(Guid vendorId);
        List<VendorDetail> GetAll();
    }
}
