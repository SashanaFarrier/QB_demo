document.addEventListener("DOMContentLoaded", () => {
    renderItemsCategoryHTML();
    renderItemsHTML();
  
    getItemsForCategoryHandler();
    getCategoryForItemHandler();

})

let itemsObj = JSON.parse(sessionStorage.getItem("items")) || {} ;

const itemsContainer = document.getElementById("items-container");
const categoriesDropdown = itemsContainer.querySelector("#category-dropdown");
const itemsDropdown = itemsContainer.querySelector("#item-dropdown");

function renderItemsHTML() {
    if (itemsObj !== null) {
        for (const items in itemsObj) {
            itemsObj[items].map(item => {
                let optionEl = `<option value="${item.Name}">${item.Name}</option>`
                itemsContainer.querySelector(".items-list").innerHTML += optionEl;
            });
        }
    }
}

function getItemsForCategoryHandler() {
    categoriesDropdown.addEventListener("change", (e) => {
        if (e.target.value != "") {
            itemsDropdown.innerHTML = `<option value="">-- Please Select --</option>`;
            Object.keys(itemsObj).map(key => {
                const subItems = itemsObj[key];
                Object.keys(subItems).map(pK => {
                    if (subItems[pK].Category == e.target.value) {
                        let optionEl = `<option value="${subItems[pK].Name}">${subItems[pK].Name}</option>`
                        itemsContainer.querySelector(".items-list").innerHTML += optionEl;
                    }

                });
            })

        } else {
            itemsDropdown.innerHTML = `<option value="">-- Please Select --</option>`;
            renderItemsHTML();
        }
    });

}

function getCategoryForItemHandler() { 
    itemsDropdown.addEventListener("change", (e) => {
        if (e.target.value != "") {
            if (itemsObj !== null) {
                var it = Object.keys(itemsObj).map(key => {
                    var subItems = itemsObj[key];
                    Object.keys(subItems).map(pk => {
                        if (subItems[pk].Name == e.target.value && (categoriesDropdown.value !== subItems[pk].Category || categoriesDropdown.value !== "")) {
                            categoriesDropdown.options[categoriesDropdown.selectedIndex].selected = false;
                            const option = Array.from(categoriesDropdown.options).find(opt => opt.innerText == subItems[pk].Category)
                            option.selected = true;
                        }
                    })

                })
            }
        } else {
            categoriesDropdown.innerHTML = `<option value="">-- Please Select --</option>`;
            renderItemsCategoryHTML();
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