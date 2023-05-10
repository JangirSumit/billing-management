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

function showLoader() {

    const loader = `
                    <div class="overlay">
                        <div class="overlay__inner">
                            <div class="overlay__content"><span class="spinner"></span></div>
                        </div>
                    </div>
                `

    document.body.innerHTML += loader;
}

function hideLoader() {
    document.body.removeChild(document.querySelector(".overlay"));
}

function validateInputFields() {
    //was-validated

    const fields = document.querySelectorAll('input[type="text"], input[type="password"], textarea, select');
    const invalidFields = Array.from(fields).filter(ele => !ele.value.trim());

    invalidFields?.forEach(ele => ele.parentElement.classList.add("was-validated"));

    return invalidFields?.length;
}

function getRandomRate(rateRange1, rateRange2) {
    if (!rateRange2) {
        return rateRange1;
    }

    const newRate = Math.ceil(
        Math.random() * (parseFloat(rateRange2) - parseFloat(rateRange1)) +
        parseFloat(rateRange1)
    );

    return Math.ceil(newRate / 100) * 100;
}

function getGUID() {
    return "xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx".replace(/[xy]/g, function (c) {
        var r = (Math.random() * 16) | 0,
            v = c == "x" ? r : (r & 0x3) | 0x8;
        return v.toString(16);
    });
}