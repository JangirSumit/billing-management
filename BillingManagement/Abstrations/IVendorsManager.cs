using BillingManagement.Models.Dto;

namespace BillingManagement.Abstrations
{
    public interface IVendorsManager
    {
        void GetVendorDetail(string gstNumber);
        Guid InsertRecord(VendorDto vendorDto);
    }
}
