namespace BillingManagement.Models.Dto;

public record ChangePasswordDto(string UserName, string CurrentPassword, string NewPassword);
