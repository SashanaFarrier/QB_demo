document.addEventListener("DOMContentLoaded", () => {
    const saveItemBtn = document.getElementById("save-item-btn")
    const customerJobInput = document.getElementById("customerJob")
    const TransactionDateInput = document.querySelector("#transactionDate input")
    
    saveItemBtn.addEventListener("click", () => {
        customerJobInput.value = customerJobInput
    });

})
