const VENDORS_API = '/api/Tokens';
const USER_DB_KEY = 'user-session';

checkActiveUser();

async function checkActiveUser() {
    var userSession = localStorage.getItem(USER_DB_KEY);

    if (userSession) {
        userSession = JSON.parse(userSession);

        if (await validateSession(userSession) == false) {
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

async function validateSession(token) {
    try {
        const response = await fetch(VENDORS_API,
            {
                headers: { Authorization: `Bearer ${token}` }
            });

        return response.ok;
    } catch (e) {
        console.error(e);
        return false
    }
}