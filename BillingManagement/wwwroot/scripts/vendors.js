const VENDORS_API = '/api/Vendors';

window.onload = async function () {
    await renderVendors();
};

/* EVENT LISTENERS */

document.getElementById("add-vendor").addEventListener("click", async function () {
    const name = document.getElementById("vendor-name").value;
    const address = document.getElementById("vendor-address").value;
    const gstNumber = document.getElementById("gst-number").value;

    if (name && address && gstNumber) {
        const data = {
            Name: name,
            Address: address,
            GstNumber: gstNumber
        }

        const result = await addVendor(data);
        if (result.id !== EMPTY_GUID) {
            await renderVendors();
        }
    }
});

document.getElementById("vendors-list").addEventListener("click", async function () {
    const vendorId = e.target.dataset.vendorId;
    if (vendorId) {
        await deleteVendor(vendorId);
        await renderVendors();
    }
});


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
                <td>${data.gstNumber}</td>
                <td>${data.adderss}</td>
                <td data-companyId="${data.id}">
                    <span class="badge bg-primary cursor-pointer" id="delete-company" data-companyId="${data.id}">X</span>
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
                    <tr class="table-primary">
                    ${ths}
                    </tr>
          </thead>`;
}


/* API CALLS */

async function addVendor(data) {
    try {
        const response = await fetch(VENDORS_API, {
            method: 'POST',
            body: JSON.stringify(data),
            headers: {
                'Content-type': 'application/json; charset=UTF-8',
            }
        });

        if (response.ok) {
            return await response.json();
        }
    } catch (e) {
        console.error(e);
    }
}

async function deleteVendor(id) {
    try {
        const response = await fetch(`${VENDORS_API}/${id}`, {
            method: 'DELETE',
        });

        if (response.ok) {
            return await response.json();
        }
    } catch (e) {
        console.error(e);
    }
}

async function getVendors() {
    try {
        const response = await fetch(VENDORS_API);
        if (response.ok) {
            return await response.json();
        }
    } catch (e) {
        console.error(e);
    }
}

async function getVendor(id) {
    try {
        const response = await fetch(`${VENDORS_API}/${id}`);
        if (response.ok) {
            return await response.json();
        }
    } catch (e) {
        console.error(e);
    }
}