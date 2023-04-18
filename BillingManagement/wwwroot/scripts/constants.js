const EMPTY_GUID = "00000000-0000-0000-0000-000000000000";

const VENDORS_API = '/api/Vendors';
const TOKENS_API = '/api/Tokens';
const USERS_API = '/api/Users';

const USER_DB_KEY = 'user-session';

const FailureReason = {
    None: 0,
    Unknown: 1,
    InvalidCredentials: 2,
    UserCreationFailed: 3,
    InvalidAccessToken: 4,
    UserAlreadyExists: 5,
    IncorrectCurrentPassword: 6
};