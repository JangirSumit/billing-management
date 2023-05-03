const EMPTY_GUID = "00000000-0000-0000-0000-000000000000";

const VENDORS_API = '/api/Vendors';
const TOKENS_API = '/api/Tokens';
const USERS_API = '/api/Users';
const ITEMS_API = '/api/Items';

const USER_DB_KEY = 'user-session';

const DESIGN_VENDORS_DB_KEY = 'design-vendors';
const DESIGN_ITEMS_DB_KEY = 'design-items';

const VENDORS_DB_KEY = 'vendors';
const ITEMS_DB_KEY = 'items';

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

const UNITS = [
    'Unknown',
    'Number(s)',
    'Kg(s)',
    'Gram(s)',
    'Liter(s)',
    'ML'
]