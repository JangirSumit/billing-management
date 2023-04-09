using BillingManagement.Models;
using BillingManagement.Models.Dto;

namespace BillingManagement.ExtensionMethods;

public static class VendorExtensions
{
    public static VendorDetail Map(this VendorDto vendor)
    {
        return new VendorDetail(vendor.Id, vendor.Name, vendor.Address, vendor.GstNumber);
    }

    public static VendorDto Map(this VendorDetail vendor)
    {
        return new VendorDto(vendor.Id, vendor.Name, vendor.Address, vendor.GstNumber);
    }
}
