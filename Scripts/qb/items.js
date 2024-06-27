const itemsContainer = document.getElementById("items-container");
const categoriesDropdown = itemsContainer.querySelector("#category-dropdown");
const itemsDropdown = itemsContainer.querySelector("#item-dropdown");

let itemsObj = {};

async function getItems() {
    if (sessionStorage.getItem("items") == null) {
        try {
            const fetchUrl = fetch("/Item/GetItems");
            const response = await fetchUrl;

            if (!response.ok) throw new Error(`Error fetching items: ${response.statusText}`)

            const items = await response.json();
            sessionStorage.setItem("items", JSON.stringify(items));
            
        } catch (error) {
            //itemsContainer.innerHTML = `<p>Error fecting items. Please try again.</p>`
            console.log("Error fetching items: ", error);
        }
    }

    itemsObj = JSON.parse(sessionStorage.getItem("items")) || {}
    renderItemsHTML();
    renderItemsCategoryHTML();
}

getItems();

function getCategories() {

    if (Object.keys(itemsObj).length === 0) return;

    return Object.keys(itemsObj).map(key => key.split("").map(letter => {
        //letter[0] = letter.toUpperCase();
        if (letter == letter.toUpperCase()) {
            let val = " " + letter
            console.log(val)
        }
    }))
   
}

getCategories()

//html render functions
function renderItemsHTML() {
    Object.keys(itemsObj).map(key => {
        const subItems = itemsObj[key];
        Object.keys(subItems).map(pK => {
            let optionEl = `<option key="${key + '_' + pK} value="${subItems[pK].Name}">${subItems[pK].Name}</option>`
            itemsContainer.querySelector(".items-list").innerHTML += optionEl;
        });
    });
}

function renderItemsCategoryHTML() {
    const itemsContainer = document.getElementById("items-container");
    const categoriesDropdown = itemsContainer.querySelector("#category-dropdown");
    //const categories = Object.keys(itemsObj).map(category =>
    //    category.split(/[A-Z]/g)
    //);

    //console.log(categories)

    const categoriesArr = ["Service", "Inventory", "Non Inventory", "Inventory Assembly", "Fixed Asset", "Discount", "Payment", "Sales Tax", "Sales Tax Group", "Group", "SubTotal", "Other Charge"];

    const categoryEl = categoriesArr.map(category => `<option>${category}</option>`);
    categoriesDropdown.innerHTML += categoryEl.join("");
}



//helper functions for filter item and category dropdown
function itemsFilter(category) {
    Object.keys(itemsObj).map(key => {
        const subItems = itemsObj[key];
        Object.keys(subItems).map(pK => {
            if (subItems[pK].Category == category) {
                let optionEl = `<option key="${key + '_' + pK} value="${subItems[pK].Name}">${subItems[pK].Name}</option>`
                itemsContainer.querySelector(".items-list").innerHTML += optionEl;
            }
        });
    });
}

function categoryFilter(item) {
    Object.keys(itemsObj).map(key => {
        const subItems = itemsObj[key];
        Object.keys(subItems).map(pk => {
            if (subItems[pk].Name == item && (categoriesDropdown.value !== subItems[pk].Category || categoriesDropdown.value !== "")) {
                categoriesDropdown.options[categoriesDropdown.selectedIndex].selected = false;
                const option = Array.from(categoriesDropdown.options).find(opt => opt.innerText == subItems[pk].Category)
                option.selected = true;
            }
        });

    })
}

//eventlisteners
function onChangeDropdown() {
    itemsContainer.addEventListener("change", (e) => {
        if (e.target.id == "item-dropdown") {
            if (e.target.value != "") {
                categoryFilter(e.target.value);
            } else {
                categoriesDropdown.innerHTML = `<option value="">-- Please Select --</option>`;
                renderItemsCategoryHTML();
            }
        }

        if (e.target.id == "category-dropdown") {
            if (e.target.value != "") {
                itemsDropdown.innerHTML = `<option value="">-- Please Select --</option>`;
                itemsFilter(e.target.value);
            } else {
                itemsDropdown.innerHTML = `<option value="">-- Please Select --</option>`;
                renderItemsHTML();
            }  
        }
    });
}

onChangeDropdown();

