async function getItems() {
   let items = sessionStorage.getItem("items");
   if(items !== null) return;
   else {
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
   
}

 getItems();