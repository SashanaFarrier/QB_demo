
document.addEventListener("DOMContentLoaded", () => {
    renderCustomerJobsHTML()
    getCustomerJobs()

});

let customerJobs = JSON.parse(sessionStorage.getItem("customers")) || [];

async function getCustomerJobs() {
    if (customerJobs.length > 0) return;

    try {
        const fetchUrl = fetch("/Customer/GetCustomerJobs");
        const response = await fetchUrl;

        if (!response.ok) throw new Error(`Error fetching items: ${response.statusText}`);

        const result = await response.json();

        sessionStorage.setItem("customers", JSON.stringify(result))
        //return result;
    }
    catch (error) {
        setTimeout(() => {
            alert("Error fecting customer jobs. Please try again.");
            console.log("Error fetching customer jobs: ", error);
        }, 30000)
    }
}

function renderCustomerJobsHTML() {
    const jobsListContainer = document.getElementById("customerJobsList");

    if (customerJobs.length > 0) {
        customerJobs.map(customerJob => {
            const optionEl = document.createElement("option")
            optionEl.text = customerJob
            jobsListContainer.querySelector(".customer-list").appendChild(optionEl)

        });

    } else {
        setTimeout(() => {
            alert("There are no customer jobs. Add new customer jobs to your QuickBooks company file and then try again")
        }, 30000)
    }
}