
document.addEventListener("DOMContentLoaded", () => {
    renderCustomerJobsHTML()
    getCustomerId();
})
async function getCustomerJobs() {
    try {
        const fetchUrl = fetch("/Customer/GetCustomerJobs");
        const response = await fetchUrl;
        const result = await response.json();

        return result;
    }
    catch (error) {
        setTimeout(() => {
            alert("Error fecting customer jobs. Please try again.");
            console.log("Error fetching customer jobs: ", error);
        }, 30000)
    }
    //return jobsListContainer;
}


function getCustomerId() {
    const customerIdInput = document.getElementById("customerId");
    const customerNameInput = document.getElementById("customerJob")
    const customerJobs = getCustomerJobs();
    customerNameInput.addEventListener("change", (e) => {
        if (e.target.value == "Select")
            customerIdInput.value = "";
        customerJobs.then(data => {
            data.filter(customerJob => {
                if (customerJob.Name == e.target.value) {
                    customerIdInput.value = customerJob.CustomerListID;
                }
            });
        })
    });
}


//functions to render html
function renderCustomerJobsHTML() {
    const jobsListContainer = document.getElementById("customerJobsList");
    const customerJobs = getCustomerJobs();

    customerJobs.then(result => {
        if (result.length > 0) {
            //const customerJobs = result.map(customerJob => `<option>${customerJob.Name}</option>`);
            result.map(customerJob => {
                if (customerJob.Locations.length > 0) {
                    const optgroupEl = document.createElement("optgroup")
                    optgroupEl.label = customerJob.Name

                    let optionEl = document.createElement("option")
                    optionEl.text = customerJob.Name

                    optgroupEl.append(optionEl)

                    const locations = customerJob.Locations;
                    locations.map(location => {
                        optionEl = document.createElement("option")
                        optionEl.text = `${customerJob.Name}:${location}`
                        optgroupEl.append(optionEl)

                    });
                    jobsListContainer.querySelector(".customer-list").appendChild(optgroupEl)

                } else {
                    const optionEl = document.createElement("option")
                    optionEl.text = customerJob.Name
                    jobsListContainer.querySelector(".customer-list").appendChild(optionEl)

                }

            });

            //jobsListContainer.querySelector(".customer-list").innerHTML += customerJobs.join("")
        } else {
            setTimeout(() => {
                alert("There are no customer jobs. Add new customer jobs to your QuickBooks company file and then try again")
            }, 30000)
        }
    })
}