window.onload = async function () {
    showLoader();
    const items = await getItems();
    const vendors = await getVendors();
    hideLoader();

    renderItemsSuggestion(items);
    renderVendorSuggestion(vendors);

    renderItems();
    renderVendors();

    attchEvents();
}

function attchEvents() {

    function createAuto(elem) {
        console.log("q");
        var input = elem;
        var dropdown = input.closest(".dropdown");
        var menu = dropdown.querySelector(".dropdown-menu");
        var listContainer = dropdown.querySelector(".list-autocomplete");
        var listItems = listContainer.querySelectorAll(".dropdown-item");
        var hasNoResults = dropdown.querySelector(".hasNoResults");

        listItems.forEach(function (item) {
            item.style.display = "none";
            item.dataset.value = item.textContent;
            // !important, keep this copy of the text outside of keyup/input function
        });

        input.addEventListener("input", function (e) {
            if ((e.keyCode ? e.keyCode : e.which) == 13) {
                input.closest(".dropdown").classList.remove("open", "in");
                return; // if enter key, close dropdown and stop
            }
            if ((e.keyCode ? e.keyCode : e.which) == 9) {
                return; // if tab key, stop
            }

            var query = input.value.toLowerCase();

            if (query.length > 1) {
                menu.classList.add("show");

                listItems.forEach(function (item) {
                    var text = item.dataset.value;
                    if (text.toLowerCase().indexOf(query) > -1) {
                        var textStart = text.toLowerCase().indexOf(query);
                        var textEnd = textStart + query.length;
                        var htmlR =
                            text.substring(0, textStart) +
                            "<em>" +
                            text.substring(textStart, textEnd) +
                            "</em>" +
                            text.substring(textEnd + length);
                        item.innerHTML = htmlR;
                        item.style.display = "";
                    } else {
                        item.style.display = "none";
                    }
                });

                var count = Array.prototype.filter.call(listItems, function (item) {
                    return item.style.display !== "none";
                }).length;
                count > 0
                    ? (hasNoResults.style.display = "none")
                    : (hasNoResults.style.display = "");
            } else {
                listItems.forEach(function (item) {
                    item.style.display = "none";
                });
                dropdown.classList.remove("open", "in");
                hasNoResults.style.display = "";
            }
        });

        listItems.forEach(function (item) {
            item.addEventListener("click", function (e) {
                const id = item.dataset.id;
                const target = item.dataset.target;

                if (target === "item") {
                    addItemToStorage(id);
                    renderItems();
                } else if (target === "vendor") {
                    addVendorToStorage(id);
                    renderVendors();
                }

                menu.classList.remove("show");
            });
        });
    }

    Array.prototype.forEach.call(
        document.querySelectorAll(".jAuto"),
        createAuto
    );

    document.addEventListener("focus", function (e) {
        if (e.target.classList.contains("jAuto")) e.target.select(); // in case input text already exists
    }, true);

    document.addEventListener("mouseup", function (e) {
        if (!e.target.closest(".dropdown")) {
            document.querySelector(".dropdown-menu").classList.remove("show");
        }
    });

    document.getElementById("add-missing-item").addEventListener("click", function () {
        if (confirm("Current changes will be lost. Are you sure you want to leave?")) {
            document.location.href = "/Items.html";
        }
    });

    document.getElementById("add-missing-vendor").addEventListener("click", function () {
        if (confirm("Current changes will be lost. Are you sure you want to leave?")) {
            document.location.href = "/Vendors.html";
        }
    });
}

function renderItemsSuggestion(items) {
    const autoComplete = document.getElementById("search-item").closest(".dropdown").querySelector(".list-autocomplete");

    let html = '';

    items.forEach(function (item) {
        html += getSeachSuggestionDataItem(item, "item");
    });

    autoComplete.innerHTML += html;
}

function renderVendorSuggestion(vendors) {
    const autoComplete = document.getElementById("search-vendor").closest(".dropdown").querySelector(".list-autocomplete");

    let html = '';

    vendors.forEach(function (item) {
        html += getSeachSuggestionDataItem(item, "vendor");
    });

    autoComplete.innerHTML += html;
}

function getSeachSuggestionDataItem(dataItem, target) {
    return `<button type="button" class="dropdown-item" data-id="${dataItem.id}" data-target="${target}">${dataItem.name}</button>`;
}

/*
 RENDER VENDORS
 */


async function renderVendors() {

    const d = await getVendorsFromStorage();

    if (d && d.length) {
        let body = "";

        const companyListHeader = getVendorListHeader(d[0])

        body = companyListHeader + "<tbody>";

        d.forEach((element, index) => {
            body += getVendorItem(element, index);
        });

        body += "</tbody>";

        document.getElementById("design-vendors-list").innerHTML = body;
    }
}

function getVendorItem(data, index) {
    return `
            <tr>
                <td scope="row">${index + 1}</td>
                <td>${data.name}</td>
                <td>${data.address}</td>
                <td>${data.gstNumber}</td>
                <td data-vendorId="${data.id}">
                    <span class="badge bg-primary cursor-pointer" id="delete-company" data-vendorId="${data.id}">X</span>
                </td>
            </tr>
    `;
}

function getVendorListHeader(data) {
    let ths = `<th scope="col">#</th>`

    Object.keys(data).forEach((d) => {
        if (d != "id") {
            ths += `<th scope="col">${capitalizeString(d)}</th>`;
        }
    });
    ths += `
          <th scope="col"></th>`;
    return `
          <thead>
                    <tr">
                    ${ths}
                    </tr>
          </thead>`;
}

/*
 RENDER ITEMS
 */

async function renderItems() {

    const d = await getItemsFromStorage();

    if (d && d.length) {
        let body = "";

        const companyListHeader = getItemListHeader(d[0])

        body = companyListHeader + "<tbody>";

        d.forEach((element, index) => {
            body += getItemListItem(element, index);
        });

        body += "</tbody>";

        document.getElementById("design-items-list").innerHTML = body;
    }
}

function getItemListItem(data, index) {
    return `
      <tr>
      <td scope="row">${index + 1}</td>
      <td>${data.name}</td>
      <td style="max-width: 400px;">${data.description}</td>
      <td>${data.unit}</td>
      <td>${data.rateRange1}</td>
      <td>${data.rateRange2}</td>
      <td>${data.cgst}</td>
      <td>${data.sgst}</td>
      <td data-item-id="${data.id
        }"><span class="badge bg-primary cursor-pointer" id="delete-item" data-itemId="${data.id
        }">X</span></td>
    </tr>
    `;
}

function getItemListHeader(data) {
    let ths = `<th scope="col">#</th>`;

    Object.keys(data).forEach((d) => {
        if (d != "id") {
            ths += `<th scope="col">${capitalizeString(d)}</th>`;
        }
    });
    ths += `
          <th scope="col"></th>`;

    return `
  <thead>
                <tr>
                  ${ths}
                </tr>
              </thead>
  `;
}

/*
 STORAGE CALLS
 */

function getVendorsFromStorage() {
    const data = localStorage.getItem(DESIGN_VENDORS_DB_KEY);

    return data && JSON.parse(data);
}

function addVendorToStorage(id) {
    let items = localStorage.getItem(DESIGN_VENDORS_DB_KEY);
    const data = getServerVendorFromStorage(id);

    if (items && data) {
        items = JSON.parse(items);
        if (items.find(it => it.id == id)) {
            return;
        }
        items = [...items, data];
    } else {
        items = [data];
    }

    localStorage.setItem(DESIGN_VENDORS_DB_KEY, JSON.stringify(items));
}

function getServerVendorFromStorage(id) {
    const result = localStorage.getItem(VENDORS_DB_KEY);
    if (result) {
        const records = JSON.parse(result);
        if (records && records.length) {
            return records.find(record => record.id == id);
        }
    }
}

function removeVendorFromStorage(id) {
    let items = localStorage.getItem(DESIGN_VENDORS_DB_KEY);
    if (items) {
        items = JSON.parse(items).filter((c) => c.id !== id);
    }
    localStorage.setItem(DESIGN_VENDORS_DB_KEY, JSON.stringify(items));
}



function getItemsFromStorage() {
    const data = localStorage.getItem(DESIGN_ITEMS_DB_KEY);

    return data && JSON.parse(data);
}

function getServerItemFromStorage(id) {
    const result = localStorage.getItem(ITEMS_DB_KEY);
    if (result) {
        const records = JSON.parse(result);
        if (records && records.length) {
            return records.find(record => record.id == id);
        }
    }
}

function addItemToStorage(id) {
    let items = localStorage.getItem(DESIGN_ITEMS_DB_KEY);
    const data = getServerItemFromStorage(id);

    if (items && data) {
        items = JSON.parse(items);
        if (items.find(it => it.id == id)) {
            return;
        }
        items = [...items, data];
    } else {
        items = [data];
    }

    localStorage.setItem(DESIGN_ITEMS_DB_KEY, JSON.stringify(items));
}

function removeItemFromStorage(id) {
    let items = localStorage.getItem(DESIGN_ITEMS_DB_KEY);
    if (items) {
        items = JSON.parse(items).filter((c) => c.id !== id);
    }
    localStorage.setItem(DESIGN_ITEMS_DB_KEY, JSON.stringify(items));
}

/*
 API CALLS
 */

async function getItems() {
    try {
        const response = await fetch(ITEMS_API, {
            headers: {
                Authorization: `Bearer ${getToken()}`
            }
        });
        if (response.ok) {
            const items = await response.json();
            localStorage.setItem(ITEMS_DB_KEY, JSON.stringify(items));
            return items;
        }
    } catch (e) {
        console.error(e);
    }
}

async function getVendors() {
    try {
        const response = await fetch(VENDORS_API, {
            headers: {
                Authorization: `Bearer ${getToken()}`
            }
        });
        if (response.ok) {
            const vendors = await response.json();
            localStorage.setItem(VENDORS_DB_KEY, JSON.stringify(vendors));
            return vendors;
        }
    } catch (e) {
        console.error(e);
    }
}