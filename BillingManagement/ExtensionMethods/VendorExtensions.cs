using BillingManagement.Models;
using BillingManagement.Models.Dto;

namespace BillingManagement.ExtensionMethods;

public static class VendorExtensions
{
    public static VendorDetail Map(this VendorDto vendor)
    {
        return new VendorDetail(vendor.Id, vendor.Name, vendor.Address, vendor.GstNumber);
    }

    public static List<VendorDetail> Map(this List<VendorDto> vendors)
    {
        List<VendorDetail> vendorDetails = new();

        foreach (VendorDto vendor in vendors)
        {
            vendorDetails.Add(new VendorDetail(vendor.Id, vendor.Name, vendor.Address, vendor.GstNumber));
        }
        return vendorDetails;
    }

    public static VendorDto Map(this VendorDetail vendor)
    {
        return new VendorDto(vendor.Id, vendor.Name, vendor.Address, vendor.GstNumber);
    }

    public static List<VendorDto> Map(this List<VendorDetail> vendors)
    {
        List<VendorDto> vendorDtos = new();

        foreach (VendorDetail vendor in vendors)
        {
            vendorDtos.Add(new VendorDto(vendor.Id, vendor.Name, vendor.Address, vendor.GstNumber));
        }

        return vendorDtos;
    }
}
