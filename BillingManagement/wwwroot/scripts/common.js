refreshToken();

function capitalizeString(str) {
    return str[0].toUpperCase() + str.substring(1);
}

async function refreshToken() {
    var token = localStorage.getItem(USER_DB_KEY);
    if (token) {
        token = JSON.parse(token);

        if (token && token.expiry && token.accessToken) {
            const timeout = new Date(token.expiry) - new Date() - 10000;

            if (timeout < 0) {
                tokenExpired();
            }

            setTimeout(async () => {
                await refresh(token.accessToken);
                refreshToken();
            }, timeout);
        }
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
            const result = await response.json();
            refreshDB(result);
        }
    } catch (e) {
        console.error(e);
    }
}

function tokenExpired() {
    window.location.href = "/Login.html";
}

async function refreshDB(token) {
    localStorage.setItem(USER_DB_KEY, JSON.stringify(token));
    await refreshToken();
}