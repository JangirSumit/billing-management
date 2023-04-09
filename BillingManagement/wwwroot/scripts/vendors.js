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
    }
});


async function addVendor(data) {
    try {
        const response = await fetch('/api/Vendors', {
            method: 'POST',
            body: JSON.stringify(data),
            headers: {
                'Content-type': 'application/json; charset=UTF-8',
            }
        });

        const result = await response.json();
        return result;

    } catch (e) {
        console.error(e);
    }
}