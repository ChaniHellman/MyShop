let totalPrice = 0
let quantity = 0

window.addEventListener("load", loadPage = () => {
    getCartItems();
    setQuantityAndTotalPrice();

});

const setQuantityAndTotalPrice = () => {
    const cartItems = JSON.parse(sessionStorage.getItem("cartItems") || "[]");
    cartItems.forEach(item => {
        quantity += item.quantity;
        totalPrice += item.quantity * item.price;
    })
    setTextTotals();
}

const setTextTotals = () => {
    document.querySelector("#itemCount").textContent = quantity 
    document.querySelector("#totalAmount").textContent = totalPrice
}

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

    let itemIndex = cartItems.findIndex(item => item.productId === id)//תזכורת: לשאול האם צריך לנהל כמות מוצר

    quantity--;
    totalPrice -= item.price;
    setTextTotals()

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

const getTodaysDate = () => {
    const today = new Date(); 
    const year = today.getFullYear(); 
    const month = String(today.getMonth() + 1).padStart(2, '0');
    const day = String(today.getDate()).padStart(2, '0');
    return `${year}-${month}-${day}`
}


const checkUserAuthentication = () => {
    const userId = sessionStorage.getItem("UserId");
    if (!userId) {
        alert("You must log in or sign up");
        window.location.href = "home.html";
        return false;
    }
    return userId;
};
const getOrderDetails = () => {
    if (!checkUserAuthentication()) return null;
    let id = sessionStorage.getItem("UserId")
    const date = getTodaysDate();
    const products = []
    const cart = JSON.parse(sessionStorage.getItem("cartItems") || "[]");
    cart.forEach(item => {
        products.push({
            "quantity": item.quantity,
            "productId": item.productId
        })
    })
    const order = {
        "orderDate": date,
        "userId": id,
        "orderSum": totalPrice,
        "orderItems": products
    }
    return order;
}


const placeOrder = async () => {

    const order = getOrderDetails();
    try {
        const responsePost = await fetch(`/api/Ordrs`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(order)
            
        });
        if (responsePost.BadRequest) {
            console.log(responsePost)
            alert("Eror,please try again")
        }
        if (!responsePost.ok)
            alert("Error, please try again")
        else {
            const orderData = await responsePost.json();  
            alert(`Order ${orderData.orderId} placed successfully!`);
            console.log("Order response:", orderData);
            sessionStorage.removeItem("cartItems");
        }
    }
    catch (error) {
        console.log(error)
    }

}