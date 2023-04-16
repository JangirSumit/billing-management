const TOKENS_API = '/api/Tokens';
const USER_DB_KEY = 'user-session';

document.getElementById("login").addEventListener("click", async function (event) {
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

document.getElementById("create-user").addEventListener("click", function () {
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