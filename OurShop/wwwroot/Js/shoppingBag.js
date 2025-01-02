
window.addEventListener("load", loadPage = () => {
    getCartItems();
});

const getCartItems = () => {
    const cartItems = JSON.parse(sessionStorage.getItem("cartItems") || "[]");
    drawCartItems(cartItems);
}

const drawCartItems = (cartItems) => {
    const template = document.querySelector("#temp-row");
    const tableBody = document.querySelector("#items tbody");
    tableBody.innerHTML = "";

    cartItems.forEach(item => {
        const cartItem = template.content.cloneNode(true);

        cartItem.querySelector(".imageColumn .image").style.backgroundImage = `url("${encodeURI(item.imageUrl)}")`;
        cartItem.querySelector(".imageColumn a").href = item.imageUrl;

        cartItem.querySelector(".descriptionColumn .itemName").textContent = item.productName;
        cartItem.querySelector(".descriptionColumn .itemNumber").textContent = item.quantity;


        cartItem.querySelector(".totalColumn.delete .expandoHeight .price").textContent = Math.round(item.price * item.quantity * 100) / 100;;

        cartItem.querySelector(".totalColumn.delete .expandoHeight .DeleteButton").addEventListener("click", (e) => { deleteFromCart(item) })

        tableBody.appendChild(cartItem);
    });
};

const deleteFromCart = (item) => {
    const cartItems = JSON.parse(sessionStorage.getItem("cartItems") || "[]");
    const id = item.productId

    itemIndex = cartItems.findIndex(item => item.productId === id)//תזכורת: לשאול האם צריך לנהל כמות מוצר

    if (item.quantity > 1) {  
        itemIndex = cartItems.findIndex(item => item.productId === id)
        cartItems[itemIndex].quantity--
        sessionStorage.setItem("cartItems", JSON.stringify(cartItems));
    }
    else {
        const updatedCartItems = cartItems.filter(item => item.productId !== id);
        sessionStorage.setItem("cartItems", JSON.stringify(updatedCartItems));
    }

    getCartItems();

}

const placeOrder = () => {
    let total = 0;
    const cartItems = JSON.parse(sessionStorage.getItem("cartItems") || "[]");
    cartItems.forEach(item => {
        total += item.quantity * item.price
    })
    console.log(`total price: ${total}`)
}