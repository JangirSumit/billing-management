document.getElementById("login")?.addEventListener("click", async function () {
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

        const userCreated = await registerUser(userName, password);

        if (userCreated) {
            const result = await loginUser(userName, password);

            if (result && result.accessToken) {
                localStorage.setItem(USER_DB_KEY, JSON.stringify(result));
                window.location.href = "/";
            }
        }

        // TODO: Handle Failed case
    }

    // TODO: Handle Validation failure
});

document.getElementById("change-password")?.addEventListener("click", async function () {
    const userName = document.getElementById("user-name").value;
    const currentPassword = document.getElementById("current-password").value;
    const newPassword = document.getElementById("new-password").value;
    const confirmPassword = document.getElementById("confirm-password").value;

    if (userName && currentPassword &&
        newPassword && confirmPassword &&
        newPassword === confirmPassword) {

    }

    // TODO: Handle Validation failure
});

document.getElementById("create-user")?.addEventListener("click", function () {
    window.location.href = "/Register.html";
});

document.getElementById("existing-user")?.addEventListener("click", function () {
    window.location.href = "/Login.html";
});

document.getElementById("login-user")?.addEventListener("click", function () {
    window.location.href = "/Login.html";
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
        const response = await fetch(`${USERS_API}/register`,
            {
                method: 'POST',
                body: JSON.stringify({ userName: userName, password: password }),
                headers: {
                    'Content-type': 'application/json; charset=UTF-8',
                }
            });

        return response.ok
    } catch (e) {
        console.error(e);
    }
}