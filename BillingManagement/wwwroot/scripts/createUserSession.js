document.getElementById("login")?.addEventListener("click", async function () {
    const userName = document.getElementById("user-name").value;
    const password = document.getElementById("password").value;

    if (userName && password) {
        const result = await loginUser(userName, password);
        if (result && result.accessToken) {
            localStorage.setItem(USER_DB_KEY, JSON.stringify(result));
            window.location.href = "/";
            return;
        } else if (result.failureReason == FailureReason.InvalidCredentials) {
            openModalDialog({
                headerType: BootstrapColor.Danger,
                title: "Invalid Credentials",
                body: "Username or Password is not correct."
            });
            return;
        }
    }

    openModalDialog({
        title: "Login",
        body: "Username or Password can not be left blank."
    });
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

        openModalDialog({
            headerType: BootstrapColor.Warning,
            title: "User Registration",
            body: "Failed to create user."
        });
        return;
    }

    openModalDialog({
        title: "User Registration",
        body: "Please check the details."
    });
});

document.getElementById("change-password")?.addEventListener("click", async function () {
    const userName = document.getElementById("user-name").value;
    const currentPassword = document.getElementById("current-password").value;
    const newPassword = document.getElementById("new-password").value;
    const confirmPassword = document.getElementById("confirm-password").value;

    if (userName && currentPassword &&
        newPassword && confirmPassword &&
        newPassword === confirmPassword) {
        const passwordChanged = await changePassword(userName, currentPassword, newPassword);

        if (passwordChanged) {
            openModalDialog({
                title: "Change Password",
                body: "Password changed successfully."
            });
            return;
        }

        openModalDialog({
            title: "Change Password",
            body: "Failed to change the password."
        });
        return;
    }

    openModalDialog({
        title: "Change Password",
        body: "Please check the details."
    });
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
                body: JSON.stringify({
                    userName: userName,
                    password: password
                }),
                headers: {
                    'Content-type': 'application/json; charset=UTF-8',
                }
            });

        return await response.json();
    } catch (e) {
        console.error(e);
    }
}

async function registerUser(userName, password) {
    try {
        const response = await fetch(`${USERS_API}/register`,
            {
                method: 'POST',
                body: JSON.stringify({
                    userName: userName,
                    password: password
                }),
                headers: {
                    'Content-type': 'application/json; charset=UTF-8',
                }
            });

        return response.ok
    } catch (e) {
        console.error(e);
    }
}

async function changePassword(userName, currentPassword, newPassword) {
    try {
        const response = await fetch(`${USERS_API}/changePassword`,
            {
                method: 'POST',
                body: JSON.stringify({
                    userName: userName,
                    currentPassword: currentPassword,
                    newPassword: newPassword
                }),
                headers: {
                    'Content-type': 'application/json; charset=UTF-8',
                }
            });

        return response.ok
    } catch (e) {
        console.error(e);
    }
}