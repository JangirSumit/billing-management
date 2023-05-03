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
            var text = item.textContent.trim();
            input.value = text;
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
