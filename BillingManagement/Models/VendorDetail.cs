namespace BillingManagement.Models;

public record VendorDetail(Guid Id, string Name, string Address, string GstNumber)
{
    public static VendorDetail Empty => new(Guid.Empty, string.Empty, string.Empty, string.Empty);
}