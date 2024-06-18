
document.addEventListener("DOMContentLoaded", () => {
    const taxesDropDown = document.getElementById("taxes-dropdown");
    const taxPercentSpan = document.getElementById("tax-percent");
    //const amountEls = Array.from(document.querySelectorAll("#amt"));
    
    getItems();
    renderItemsCategoryHTML();
    renderItemsHTML()
    getTaxes()

   
    //amountEls.forEach(el => {
    //    el.addEventListener("input", (e) => {
    //        let inputVal = Number(e.target.value)
    //        let rate = inputVal / Number(quantityEl.value)

    //        if ((e.target.value != "" && quantityEl.value != "")) {
    //            el.parentElement.nextElementSibling.querySelector("#rate").value = rate
    //        }
    //    })
    //})
   // addRateTaxAmount();
})

async function getItems() {
    try {
        const fetchUrl = fetch("/Item/GetItems");
        const response = await fetchUrl;
        const result = await response.json();

        return result
    }
    catch (error) {
        //itemsContainer.innerHTML = `<p>Error fecting items. Please try again.</p>`
        console.log("Error fetching items: ", error);
    }
}

const itemsArr = getItems();
function renderItemsHTML() {
    const itemsContainer = document.getElementById("items-container");
    const categoriesDropdown = itemsContainer.querySelector("#category-dropdown");
    const itemsDropdown = itemsContainer.querySelector("#item-dropdown");
    const descriptionEl = document.getElementById("desc");
    const taxEl = document.getElementById("tax");
    const rateEl = document.getElementById("rate");
    const amountEl = document.getElementById("amt");
    const quantityEl = document.getElementById("qty");


    //add items to items dropdown by default
    const itemsDispay = () => {
        itemsArr.then(result => {
            if(result.length > 0) {
                for (const items in result) {
                    result[items].map(item => {
                        let optionEl = `<option>${item.Name}</option>`
                        itemsContainer.querySelector(".items-list").innerHTML += optionEl;
                    })
                }
            }
        });
    }

    //itemsDispay();

    categoriesDropdown.addEventListener("change", (e) => {
        //clickedFirst = "category dropdown";
        //itemsDropdown.innerHTML = ""
        
        if (e.target.value != "Select") {
            //itemsDropdown.innerHTML = ""
            //console.log(itemsDropdown.innerHTML)
            itemsDropdown.innerHTML = `<option>Select</option>`;
            itemsArr.then(data => {
                for (const items in data) {
                    data[items].filter(item => {
                        if (item.Category == e.target.value) {
                            let optionEl = `<option>${item.Name}</option>`
                            itemsContainer.querySelector(".items-list").innerHTML += optionEl;
                        }
                    });
                }
            });
        } else {
            itemsDropdown.innerHTML = ""
            itemsDispay();
        }
    });
   

    itemsDropdown.addEventListener("click", (e) => {
        if (e.target.value != "Select") {
            itemsArr.then(data => {

                //get taxes 
                getTaxes(data.salesTax)
                //const taxes = data.salesTax
                //const taxesEl = taxes.map(tax => `<option>${tax.Name}</option>`)
                //taxesDropDown.innerHTML += taxesEl.join("")

                for (const items in data) {
                    
                    data[items].filter(item => {
                        if (item.Name == e.target.value) {
                            console.log(item)
                            item.Description ? descriptionEl.value = item.Description : descriptionEl.value = "";

                            item.Tax ? taxEl.value = item.Tax : taxEl.value = "Non";

                            item.Amount && item.Amount > 0 ? amountEl.value = item.Amount : 
                            item.Rate > 0 && +quantityEl.value > 0 ?
                            amountEl = item.Rate * +quantityEl.value :
                            amountEl.value = ""

                            item.Rate && item.Rate > 0 ? rateEl.value = item.Rate :
                            item.Amount > 0 && Number(quantityEl.value) > 0 ?
                            rateEl.value = item.Amount / Number(quantityEl.value) :
                            rateEl.value = "";
                        }
                    })
                }
            })
        }
    })

    amountEl.addEventListener("input", (e) => {
        let inputVal = Number(e.target.value)
        let rate = inputVal / Number(quantityEl.value)
        console.log(rate)

        if ((e.target.value != "" && quantityEl.value != "")) {
            rateEl.value = rate.toFixed(2)
        }
    });

   
   
}



function renderItemsCategoryHTML() {
    const itemsContainer = document.getElementById("items-container");
    const categoriesDropdown = itemsContainer.querySelector("#category-dropdown");

    const categoriesArr = ["Service", "Inventory", "Non Inventory", "Inventory Assembly", "Fixed Asset", "Discount", "Payment", "Sales Tax", "Sales Tax Group", "Group", "SubTotal", "Other Charge"];

    const categoryEl = categoriesArr.map(category => `<option>${category}</option>`);
    categoriesDropdown.innerHTML += categoryEl.join("");
}

function getTaxes() {
    const taxesDropDown = document.getElementById("taxes-dropdown");
    const taxPercentSpan = document.getElementById("tax-percent");
    const totalCost = document.getElementById("order-total")

    if (taxesDropDown != null) {
        itemsArr.then(data => {
            const taxes = data.salesTax
            console.log(taxes)
            const taxesEl = taxes.map(tax => `<option>${tax.Name}</option>`)
            taxesDropDown.innerHTML += taxesEl.join("")

            taxesDropDown.addEventListener("change", (e) => {
                const tax = e.target.value;
                taxes.filter(t => {
                    if (t.Name == tax) {
                        taxPercentSpan.textContent = `(${t.Tax}%)`

                        const total = (Number(totalCost.textContent.slice(1)) * Number(t.Tax)) + Number(totalCost.textContent.slice(1))
                        console.log(total)
                        totalCost.textContent = total
                    }
                })
                //console.log(tax)

            })

        })
    }
    
}