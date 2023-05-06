checkActiveUser();

async function checkActiveUser() {
    var userSession = localStorage.getItem(USER_DB_KEY);

    if (userSession) {
        userSession = JSON.parse(userSession);

        if (!userSession['accessToken']) {
            rediretToLogin();
        } else if (new Date(userSession.expiry) - new Date() <= 0 &&
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
    return await REST_API.get(`${TOKENS_API}/validate`);
}