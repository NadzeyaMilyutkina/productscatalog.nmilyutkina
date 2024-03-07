// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function filterCategories() {
    // Declare variables
    var input, filter, table, tr, td, i, txtValue;
    input = document.getElementById("categoriesFilter");
    filter = input.value.toUpperCase();
    table = document.getElementById("category");
    tr = table.getElementsByTagName("tr");

    // Loop through all table rows, and hide those who don't match the search query
    for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[0];
        if (td) {
            txtValue = td.textContent || td.innerText;
            if (txtValue.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
            }
        }
    }
}

async function convertToUsd(target, bynValue) {
    const rate = await getRate();
    const price = parseFloat(bynValue) / parseFloat(rate);
    target.textContent = "($ USD: " + Math.round(price * 100) / 100 + ")";
}

function cleanUp(target) {
    target.textContent = "*";
}

function getRate() {
    // if (caches.has(`rate`)) {
    //     return caches['rate'];
    // }

    let requestUrl = "https://api.nbrb.by/exrates/rates/840?parammode=1";

    return fetch(requestUrl)
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json();
        })
        .then(data => {
            var rate = data.Cur_OfficialRate;
            console.log(data.Cur_OfficialRate);
            // caches.open("rate").then(value => rate);
            return rate;
        })
        .catch(error => {
            console.error('Error:', error);
        });
}
