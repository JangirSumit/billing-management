namespace BillingManagement.Contracts.Enums;

public enum FailureReason
{
    None = 0,
    Unknown,
    InvalidCredentials,
    UserCreationFailed,
    InvalidAccessToken,
    UserAlreadyExists,
    IncorrectCurrentPassword
}
