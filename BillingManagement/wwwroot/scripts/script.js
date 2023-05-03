renderHeader();
activateHeader();
renderFooter();

document.getElementById("logout").addEventListener("click", function () {
    localStorage.removeItem(USER_DB_KEY);
    window.location.href = "/Login.html"
})

function renderHeader() {
    const headerHtml = `
            <nav class="navbar navbar-expand-lg bd-navbar sticky-top bg-dark border-bottom box-shadow mb-3" data-bs-theme="dark">
            <div class="container">
                <a class="navbar-brand" href="/"> 
                    <img
                    src="../images/quote.ico"
                    alt="Logo"
                    width="32"
                    height="32"
                    class="d-inline-block align-text-top"
                  /> Billing Management</a>
                <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target=".navbar-collapse" aria-controls="navbarSupportedContent"
                        aria-expanded="false" aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="navbar-collapse collapse d-sm-inline-flex justify-content-between">
                    <ul class="navbar-nav flex-grow-1">
                        <li class="nav-item">
                            <a class="nav-link text-white" href="/Index.html">Home</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link text-white" href="/Venders.html">Venders</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link text-white" href="/Items.html">Items</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link text-white" href="/Design.html">Design</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link text-white" href="/Help.html">Help</a>
                        </li>
                        
                    </ul>
                    <ul class="navbar-nav flex-row flex-wrap ms-md-auto">
                        <li class="nav-item" style="float: right; width: 100%;">
                            <button type="button" class="btn btn-light" id="logout">Logout</button>
                        </li>
                    </ul>
                </div>
            </div>
        </nav>
    `;

    document.querySelector("header").innerHTML = headerHtml;
}

function activateHeader() {
    const pathName = window.location.pathname;

    const tab = document.querySelector(`nav a[href='${pathName}']`);
    if (tab) {
        tab.classList.add("active");
        tab.setAttribute("aria-current", true);
    }
}

function renderFooter() {
    const footerHtml = `
      <ul class="nav justify-content-center border-bottom pb-3 mb-3">
              <!--<li class="nav-item"><a href="#" class="nav-link px-2 text-body-secondary">Home</a></li>
              <li class="nav-item"><a href="#" class="nav-link px-2 text-body-secondary">Features</a></li>
              <li class="nav-item"><a href="#" class="nav-link px-2 text-body-secondary">Pricing</a></li>
              <li class="nav-item"><a href="#" class="nav-link px-2 text-body-secondary">FAQs</a></li>
              <li class="nav-item"><a href="#" class="nav-link px-2 text-body-secondary">About</a></li>-->
            </ul>
        <p class="text-center text-body-secondary"><b>© 2023, Jangir Technologies</b></p>
    `;

    document.querySelector("footer").innerHTML = footerHtml;
}

function getToken() {
    const USER_DB_KEY = 'user-session';
    var userSession = localStorage.getItem(USER_DB_KEY);

    if (userSession) {
        userSession = JSON.parse(userSession);
        return userSession.accessToken;
    }
    return "";
}