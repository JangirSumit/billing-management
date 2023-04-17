refreshToken();

function capitalizeString(str) {
    return str[0].toUpperCase() + str.substring(1);
}

async function refreshToken() {
    var token = localStorage.getItem(USER_DB_KEY);
    if (token) {
        token = JSON.parse(token);
        const timeout = new Date(token.expiry) - new Date() - 10000;

        if (timeout < 0) {
            tokenExpired();
        }

        const refreshTonenInterval = setInterval(function () {
            refresh(token.accessToken);
            clearInterval(refreshTonenInterval);
            refreshToken();
        }, timeout);
    }
}

async function refresh(token) {
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

function tokenExpired() {
    window.location.href = "/Login.html";
}

function refreshDB(token) {
    localStorage.setItem(USER_DB_KEY, JSON.stringify(token));
    refreshToken();
}