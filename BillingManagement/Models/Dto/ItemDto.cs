using BillingManagement.Enums;

namespace BillingManagement.Models.Dto;

public record ItemDto(Guid Id, string Name, string Description, ItemUnit Unit, double RateRange1, double RateRange2, double Sgst, double Cgst);
