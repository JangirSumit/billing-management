renderHeader();
activateHeader();
renderFooter();

function renderHeader() {
    const headerHtml = `
            <nav class="navbar navbar-expand-lg bd-navbar sticky-top bg-dark border-bottom box-shadow mb-3" data-bs-theme="dark">
            <div class="container">
                <a class="navbar-brand">Billing Management</a>
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
                            <a class="nav-link text-white" href="/Workflow.html">Workflow</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link text-white" href="/Help.html">Help</a>
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

}