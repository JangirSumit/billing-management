window.onload = async function () {
    showLoader();
    await renderVendors();
    hideLoader();

    attachEvents();
};

/* EVENT LISTENERS */

function attachEvents() {

    document.getElementById("add-vendor").addEventListener("click", async function () {
        const name = document.getElementById("vendor-name").value;
        const address = document.getElementById("vendor-address").value;
        const gstNumber = document.getElementById("gst-number").value;

        if (name && address && gstNumber) {
            showLoader();
            const data = {
                Name: name,
                Address: address,
                GstNumber: gstNumber
            }

            const result = await addVendor(data);
            if (result) {
                await renderVendors();
            }

            hideLoader();
        }
    });

    document.getElementById("vendors-list").addEventListener("click", async function (e) {
        const vendorId = e.target.dataset.vendorid;
        if (vendorId) {
            showLoader();
            await deleteVendor(vendorId);
            await renderVendors();
            hideLoader();
        }
    });
}

/* INTERNALS */

async function renderVendors() {

    const d = await getVendors();

    if (d && d.length) {
        let body = "";

        const companyListHeader = getVendorListHeader(d[0])

        body = companyListHeader + "<tbody>";

        d.forEach((element, index) => {
            body += getVendorItem(element, index);
        });

        body += "</tbody>";

        document.getElementById("vendors-list").innerHTML = body;
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


/* API CALLS */

async function addVendor(data) {
    return await REST_API.post(VENDORS_API, data);
}

async function deleteVendor(id) {
    return await REST_API.delete(`${VENDORS_API}/${id}`);
}

async function getVendors() {
    return await REST_API.get(VENDORS_API);
}

async function getVendor(id) {
    return await REST_API.get(`${VENDORS_API}/${id}`);
}