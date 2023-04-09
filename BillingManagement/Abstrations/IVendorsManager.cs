using BillingManagement.Models.Dto;

namespace BillingManagement.Abstrations
{
    public interface IVendorsManager
    {
        void GetVendorByGstNumber(string gstNumber);
        Guid Add(VendorDto vendorDto);

        VendorDto
    }
}
