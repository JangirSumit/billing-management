namespace BillingManagement.Dto;

public record ChangePasswordDto(string UserName, string CurrentPassword, string NewPassword);
