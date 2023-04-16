const EMPTY_GUID = "00000000-0000-0000-0000-000000000000";
const USER_DB_KEY = 'user-session';
const TOKENS_API = '/api/Tokens';

refreshToken();

function capitalizeString(str) {
    return str[0].toUpperCase() + str.substring(1);
}

async function refreshToken() {
    var token = localStorage.getItem(USER_DB_KEY);
    if (token) {
        token = JSON.parse(token);
        const timeout = new Date(token.expiry) - new Date();

        if (timeout < 0) {
            refresh();
        }

        const refreshTonenInterval = setInterval(function () {
            refresh();
            clearInterval(refreshTonenInterval);
        }, timeout - 1000);
    }
}

function refresh(token) {
    try {
        const response = await fetch(`${TOKENS_API}/refresh`,
            {
                method: 'GET',
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });

        if (response.ok) {
            refreshDB(response.json());
        }
    } catch (e) {
        console.error(e);
    }
}

function refreshDB(token) {
    localStorage.setItem(USER_DB_KEY, JSON.stringify(token));
    refreshToken();
}