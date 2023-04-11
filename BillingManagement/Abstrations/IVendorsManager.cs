using BillingManagement.Models;

namespace BillingManagement.Abstrations
{
    public interface IVendorsManager
    {
        void GetByGstNumber(string gstNumber);
        VendorDetail Add(VendorDetail vendorDto);
        VendorDetail GetById(Guid vendorId);
        List<VendorDetail> GetAll();
        bool Delete(Guid id);
    }
}
