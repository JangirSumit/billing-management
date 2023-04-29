const EMPTY_GUID = "00000000-0000-0000-0000-000000000000";

const VENDORS_API = '/api/Vendors';
const TOKENS_API = '/api/Tokens';
const USERS_API = '/api/Users';
const ITEMS_API = '/api/Items';

const USER_DB_KEY = 'user-session';

const FailureReason = {
    None: 'None',
    Unknown: 'Unknown',
    InvalidCredentials: 'InvalidCredentials',
    UserCreationFailed: 'UserCreationFailed',
    InvalidAccessToken: 'InvalidAccessToken',
    UserAlreadyExists: 'UserAlreadyExists',
    IncorrectCurrentPassword: 'IncorrectCurrentPassword'
};

const BootstrapColor = {
    Primary: 'primary',
    Secondary: 'secondary',
    Success: 'success',
    Danger: 'danger',
    Warning: 'warning',
    Info: 'info',
    Light: 'light',
    Dark: 'dark'
}