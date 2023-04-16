const TOKENS_API = '/api/Tokens';
const USER_DB_KEY = 'user-session';

document.getElementById("login")?.addEventListener("click", async function (event) {
    const userName = document.getElementById("user-name").value;
    const password = document.getElementById("password").value;

    if (userName && password) {
        const result = await loginUser(userName, password);
        if (result && result.accessToken) {
            localStorage.setItem(USER_DB_KEY, JSON.stringify(result));
            window.location.href = "/";
        }
    }
});

document.getElementById("register")?.addEventListener("click", async function () {
    const userName = document.getElementById("user-name").value;
    const password = document.getElementById("password").value;
    const confirmPassword = document.getElementById("confirm-password").value;

    if (userName && password && confirmPassword &&
        password === confirmPassword) {
        const result = await registerUser(userName, password);
        if (result && result.accessToken) {
            localStorage.setItem(USER_DB_KEY, JSON.stringify(result));
            window.location.href = "/";
        }
    }
});

document.getElementById("create-user")?.addEventListener("click", async function () {
    window.location.href = "/Register.html";
});

async function loginUser(userName, password) {
    try {
        const response = await fetch(TOKENS_API,
            {
                method: 'POST',
                body: JSON.stringify({ userName: userName, password: password }),
                headers: {
                    'Content-type': 'application/json; charset=UTF-8',
                }
            });

        if (response.ok) {
            return response.json();
        }
    } catch (e) {
        console.error(e);
    }
}

async function registerUser(userName, password) {
    try {
        const response = await fetch(`${TOKENS_API}/register`,
            {
                method: 'POST',
                body: JSON.stringify({ userName: userName, password: password }),
                headers: {
                    'Content-type': 'application/json; charset=UTF-8',
                }
            });

        if (response.ok) {
            return response.json();
        }
    } catch (e) {
        console.error(e);
    }
}