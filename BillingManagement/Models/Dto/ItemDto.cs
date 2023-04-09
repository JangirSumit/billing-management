using BillingManagement.Enums;

namespace BillingManagement.Models.Dto
{
    public record ItemDto(Guid Id, string Name, string Description, int Quantity, ItemUnit Unit, double Rate);
}
