﻿window.onload = async function () {
    showLoader();
    await renderItems();
    hideLoader();

    attachEvents();
};

function attachEvents() {
    document.getElementById("items-list").addEventListener("click", async function (e) {
        const itemId = e.target.dataset.itemid;
        if (itemId) {
            showLoader();
            await deleteItem(itemId);
            await renderItems();
            hideLoader();
        }
    });

    document.querySelectorAll(".item-gst")?.forEach(i => i.addEventListener("change", function (event) {
        const value = parseFloat(event.target.value || 0);

        if (value < 0 && value > 100) {
            openModalDialog({
                headerType: BootstrapColor.Info,
                title: "Create Item",
                body: "GST number can not be greater than 100 and less than 0."
            });
        }
    })
    );

    document.getElementById("add-item").addEventListener("click", async function () {

        const name = document.getElementById("item-name").value;
        const description = document.getElementById("item-description").value;
        const unit = document.getElementById("item-unit").value;
        const rateRange1 = document.getElementById("item-rate-range-1").value;
        const rateRange2 = document.getElementById("item-rate-range-2").value;
        const cgst = document.getElementById("item-cgst").value;
        const sgst = document.getElementById("item-sgst").value;

        if (name && description && unit && rateRange1 && rateRange2 && cgst && sgst) {
            showLoader();

            const result = await addItem({
                name: name,
                description: description,
                unit: parseInt(unit),
                rateRange1: rateRange1,
                rateRange2, rateRange2,
                sgst: sgst,
                cgst: cgst
            });

            if (result) {
                await renderItems();
            }

            hideLoader();
        }
        else {
            openModalDialog({
                headerType: BootstrapColor.Warning,
                title: "Create Item",
                body: "All fields are mandetory fields. Please fill the details."
            });
        }
    });
}

/* INTERNALS */

async function renderItems() {

    const d = await getItems();

    if (d && d.length) {
        let body = "";

        const companyListHeader = getItemListHeader(d[0])

        body = companyListHeader + "<tbody>";

        d.forEach((element, index) => {
            body += getItemListItem(element, index);
        });

        body += "</tbody>";

        document.getElementById("items-list").innerHTML = body;
    }
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


/* API CALLS */

async function addItem(data) {
    return await REST_API.post(ITEMS_API, data);
}

async function getItems() {
    return await REST_API.get(ITEMS_API);
}

async function deleteItem(id) {
    return await REST_API.delete(`${ITEMS_API}/${id}`);
}