
document.addEventListener("DOMContentLoaded", () => {
    const taxesDropDown = document.getElementById("taxes-dropdown");
    const taxPercentSpan = document.getElementById("tax-percent");
    //const amountEls = Array.from(document.querySelectorAll("#amt"));
   
    //getItems();
    renderItemsCategoryHTML();
    renderItemsHTML()
    getTaxes()

})

const itemsArr = getItems();

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
                        let optionEl = `<option value="${item.Name}">${item.Name}</option>`
                        itemsContainer.querySelector(".items-list").innerHTML += optionEl;
                    })
                }
            }
        });
    }

    categoriesDropdown.addEventListener("change", (e) => {
        //clickedFirst = "category dropdown";
        //itemsDropdown.innerHTML = ""
        
        if (e.target.value != "") {
            //itemsDropdown.innerHTML = ""
            //console.log(itemsDropdown.innerHTML)
            itemsDropdown.innerHTML = `<option value="">-- Please Select --</option>`;
            itemsArr.then(data => {
                for (const items in data) {
                    data[items].filter(item => {
                        if (item.Category == e.target.value) {
                            let optionEl = `<option value="${item.Name}">${item.Name}</option>`
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
        if (e.target.value != "") {
            itemsArr.then(data => {
               
                for (const items in data) {
                    
                    data[items].filter(item => {
                        if (item.Name == e.target.value) {
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
    const totalCost = document.getElementById("order-total");
    const tableTaxInputEls = Array.from(document.querySelectorAll("[data-title='Tax'] input"));
   
    if (taxesDropDown != null) {
        if (tableTaxInputEls.every(el => el.value == "Non")) {
            return;
        } else {
            itemsArr.then(data => {
                console.log(data)
                const taxes = data.salesTax
                const salesTaxGroup = data.salesTaxGroup
                taxes.push(...salesTaxGroup)
                //console.log(taxes)
                const taxesEl = taxes.map(tax => `<option>${tax.Name}</option>`)
                //const taxGroupEl = salesTaxGroup.map(taxGroup => `<option>${taxGroup.Name}</option>`)
                taxesDropDown.innerHTML += taxesEl.join("")
                //taxesDropDown.innerHTML += taxGroupEl.join("")
                
                taxesDropDown.addEventListener("change", (e) => {
                    const tax = e.target.value;
                    taxes.filter(t => {
                        if (t.Name == tax) {
                            taxPercentSpan.textContent = `(${t.Tax}%)`
                            const percentage = ((Number(t.Tax) / 100) * Number(totalCost.textContent.slice(1))) / 2
                            let total = Number(totalCost.textContent.slice(1)) + percentage
                          
                            total = total.toLocaleString('en-US', {
                                style: 'currency',
                                currency: 'USD',
                            });

                            totalCost.textContent = total
                        }
                    })
                   
                })

            })
        }
       
    }
    
}