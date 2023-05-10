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
    renderTextEditor();
}

function attchEvents() {

    function createAuto(elem) {
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

                input.value = "";
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

    document.getElementById("design-items-list").addEventListener("click", async function (e) {
        const itemId = e.target.dataset.itemid;
        const domId = e.target.id;

        if (domId === "delete-item" && itemId) {
            showLoader();
            await removeItemFromStorage(itemId);
            await renderItems();
            hideLoader();
        }
    });

    document.getElementById("design-vendors-list").addEventListener("click", async function (e) {
        const itemId = e.target.dataset.vendorid;
        if (itemId) {
            showLoader();
            await removeVendorFromStorage(itemId);
            await renderVendors();
            hideLoader();
        }
    });

    document.getElementById("generate-pdf").addEventListener("click", function () {
        generatePDF();
    });

    document.getElementById("preview-pdf").addEventListener("click", function () {
        preivewPDF();
    });
}

function addEventListnerForQuantities() {
    const itemQuantityTexts = document.querySelectorAll(".item-quantity");

    Array.from(itemQuantityTexts).forEach(function (textbox) {
        textbox.addEventListener("change", function () {
            changeItemQuantity(textbox.dataset.itemid, textbox.value);
        });
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

function renderTextEditor() {
    tinymce.init({
        selector: '#pdf-terms-conditions',
        menubar: '',
        plugins: 'anchor autolink charmap codesample emoticons image link lists media searchreplace table visualblocks wordcount checklist mediaembed casechange export formatpainter pageembed linkchecker a11ychecker tinymcespellchecker permanentpen powerpaste advtable advcode editimage tinycomments tableofcontents footnotes mergetags autocorrect typography inlinecss',
        toolbar: 'undo redo | blocks fontfamily fontsize | bold italic underline strikethrough | link image media table mergetags | addcomment showcomments | spellcheckdialog a11ycheck typography | align lineheight | checklist numlist bullist indent outdent | emoticons charmap | removeformat',
        tinycomments_mode: 'embedded',
        setup: function (editor) {
            editor.on('init', function (e) {
                editor.setContent(`<p><strong>Terms and Conditions</strong></p>
                                        <ul><li>The rates are valid for a period of 30 days only.</li>
                                            <li>The rates include delivery at the site including loading and unloading for delivery within the limits of the greater Hyderabad municipal corporation region.</li>
                                            <li>The rates are including hardware, supply, local transport, and complete installations.</li>
                                            <li>All items shall be fully completed at our factory in all aspects including polishing and shall be packed and delivered after inspection and approval by a client representative.Items once delivered shall not be accepted for return.</li>
                                        </ul>`);
            });
        }
    });
}

/*
 RENDER VENDORS
 */


async function renderVendors() {

    const d = await getVendorsFromStorage();
    let body = "";

    if (d && d.length) {

        const companyListHeader = getVendorListHeader(d[0])

        body = companyListHeader + "<tbody>";

        d.forEach((element, index) => {
            body += getVendorItem(element, index);
        });

        body += "</tbody>";
    }

    document.getElementById("design-vendors-list").innerHTML = body;
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
    let body = "";

    if (d && d.length) {

        const companyListHeader = getItemListHeader(d[0])

        body = companyListHeader + "<tbody>";

        d.forEach((element, index) => {
            body += getItemListItem(element, index);
        });

        body += "</tbody>";
    }
    document.getElementById("design-items-list").innerHTML = body;

    addEventListnerForQuantities();
}

function getItemListItem(data, index) {
    return `
          <tr>
          <td scope="row">${index + 1}</td>
          <td>${data.name}</td>
          <td style="max-width: 400px;">${data.description}</td>
          <td>${UNITS[data.unit]}</td>
          <td>${data.rateRange1}</td>
          <td>${data.rateRange2}</td>
          <td>${data.cgst}</td>
          <td>${data.sgst}</td>
          <td><div class="input-group input-group-sm" style="width: 60px;">
              <input type="number" class="form-control item-quantity" aria-label="Sizing example input"
                    aria-describedby="inputGroup-sizing-sm" data-itemid="${data.id}" value="${data.quantity}"/>
            </div>
        </td>
          <td data-item-id="${data.id
        }"><span class="badge bg-primary cursor-pointer" id="delete-item" data-itemId="${data.id
        }">X</span></td>
        </tr>
    `;
}

function getItemListHeader(data) {
    let ths = `<th scope="col">#</th>`;

    Object.keys(data).forEach((d) => {
        if (d == 'rateRange1') {
            d = 'Rate (Min)'
        } else if (d == 'rateRange2') {
            d = 'Rate (Max)'
        }

        if (d != "id") {
            ths += `<th scope="col">${capitalizeString(d)}</th>`;
        }
    });
    ths += `<th scope="col">Quantity</th>
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

function changeItemQuantity(itemId, value) {
    let items = localStorage.getItem(DESIGN_ITEMS_DB_KEY);
    if (items) {
        const item = JSON.parse(items).find((c) => c.id == itemId);

        item.quantity = value;

        items = [...JSON.parse(items).filter((c) => c.id != itemId), item];

        localStorage.setItem(DESIGN_ITEMS_DB_KEY, JSON.stringify(items));
    }
}

/*
 API CALLS
 */

async function getItems() {
    const items = await REST_API.get(ITEMS_API);

    localStorage.setItem(ITEMS_DB_KEY, JSON.stringify(items));
    return items;
}

async function getVendors() {
    const vendors = await REST_API.get(VENDORS_API);

    localStorage.setItem(VENDORS_DB_KEY, JSON.stringify(vendors));
    return vendors;
}

/*
 * PDF
 */

function preivewPDF() {
    const items = getItemsFromStorage();
    const vendors = getVendorsFromStorage();

    const pdfHTML = getHTML(vendors[0], items)

    if (pdfHTML) {
        openModalDialog({
            size: "modal-xl",
            title: "Preview PDF",
            body: pdfHTML.innerHTML
        });
    }
}

function generatePDF() {
    window.jsPDF = window.jspdf.jsPDF;

    const items = getItemsFromStorage();
    const vendors = getVendorsFromStorage();


    generate(getHTML(vendors[0], items), vendors[0].name);

    // GENERATE MULTIPLE
    //vendors.forEach(function (vendor) {
    //    generate(getHTML(vendor, items), vendor.name);
    //});

}

function generate(viewHtml, name) {
    const doc = new jsPDF();
    doc.html(viewHtml, {
        callback: function (doc) {
            // Save the PDF
            doc.save(`${name}.pdf`);
        },
        x: 15,
        y: 15,
        width: 170,
        windowWidth: 650
    });
}

function getHTML(vendor, items) {

    if (!validateDataBeforePdf()) {
        openModalDialog({
            title: "Pdf Generation",
            body: "Please check the details."
        });
        return;
    }

    const element = document.createElement('div');
    element.id = `temp-pdf-${vendor.id}`;

    let viewHtml = `${getHeaderNote(vendor)}
                    ${getItemsForPdf(items)}
                    ${getTermsAndConditions()}
                    ${getSignature(vendor.name)}
                    ${getFooterAddress(vendor)}`;

    element.innerHTML = viewHtml;
    return element;
}

function validateDataBeforePdf() {
    return document.getElementById("pdf-project-name").value;
}

function getHeaderNote(vendor) {
    return `<p class="fw-bold" style="margin:0px; padding: 5px 0px;">${vendor.name}</p>
            <p class="fw-bold" style="margin:0px; padding: 5px 0px;">${vendor.gstNumber}</p>
            <p class="fw-bold" style="margin:0px; padding: 5px 0px;">${getDate()}</p>
            <p class="text-center fw-bold" style="font-size:16px; margin:0px; padding: 5px 0px;">QUOTATION</p>
            <p class="fw-bold">${document.getElementById("pdf-project-name").value}<p>`;
}

function getFooterAddress(vendor) {
    return `<p class="text-center class="fw-bold"">${vendor.address}</p>`;
}

function getDate() {
    return new Date().toLocaleString().split(',')[0];
}

function getTermsAndConditions() {
    return tinymce.activeEditor.getContent();
}

function getItemsForPdf(items) {
    let tableHtml = `<table class="table">${getTableHeader()}`;

    tableHtml += `<tbody>`;

    items.forEach((item, index) => {
        tableHtml += getTableItemRow(item, index);
    });

    tableHtml += `</tbody></table>`;

    return tableHtml;
}

function getSignature(name) {
    return `<div class="float-end border-top border-dark border-3" style="padding-left: 10px; padding-right: 10px;">
        For ${name}
    </div><br/>`;
}

function getTableHeader() {
    return `<thead>
        <th>S. no.</th>
        <th>Product</th>
        <th>Qty</th>
        <th>Unit</th>
        <th>Amount</th>
    </thead>`;
}

function getTableItemRow(item, index) {
    return `<tr>
        <td>${index + 1}</td>
        <td><strong>${item.name}</strong>
            <p>${item.description}</p></td>
        <td>${item.quantity}</td>
        <td>${item.unit}</td>
        <td>${item.quantity * item.rateRange1}</td>
    </tr>`;
}