const TOKENS_API = '/api/Tokens';
const USER_DB_KEY = 'user-session';

checkActiveUser();

async function checkActiveUser() {
    var userSession = localStorage.getItem(USER_DB_KEY);

    if (userSession) {
        userSession = JSON.parse(userSession);

        if (new Date(userSession.expiry) - new Date() <= 0 &&
            await validateSession(userSession.accessToken) == false) {
            rediretToLogin();
        } 
    }
    else {
        rediretToLogin();
    }
}

function rediretToLogin() {
    window.location.href = "/Login.html";
}

function redirectToHome() {
    window.location.href = "/";
}

async function validateSession(token) {
    try {
        const response = await fetch(`${TOKENS_API}/validate`,
            {
                headers: { Authorization: `Bearer ${token}` }
            });

        return response.ok;
    } catch (e) {
        console.error(e);
        return false
    }
}