namespace BillingManagement.Models;

public record UserDetail(string UserName, string Password)
{
    public static UserDetail Empty => new("", "");
}

