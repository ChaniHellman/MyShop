﻿
window.addEventListener("load", loadPage = () => {
    getProducts();
    getCategories();
    const urlParams = new URLSearchParams(window.location.search);
    const fromShoppingBag = urlParams.get("fromShoppingBag");

    if (fromShoppingBag === "1") {
        const cartItems = JSON.parse(sessionStorage.getItem("cartItems") || "[]");
        sessionStorage.setItem("cartItems", JSON.stringify(cartItems));
        let bagSum = 0 //המספר על אייקון הסל
        cartItems.forEach(item => {
            bagSum += item.quantity
        })
        document.querySelector("#ItemsCountText").textContent = bagSum
    }
    else
        sessionStorage.setItem("cartItems", "")
});

let categoryIds = [];
let paramObject = { desc:null, minPrice:null, maxPrice:null, categoryIds:null }


const getProducts = async () => {

    const params = new URLSearchParams();

    if (paramObject.desc) params.append("desc", paramObject.desc);
    if (paramObject.minPrice) params.append("minPrice", paramObject.minPrice);
    if (paramObject.maxPrice) params.append("maxPrice", paramObject.maxPrice);
    if (paramObject.categoryIds && categoryIds.length > 0) {

        categoryIds.forEach(id => params.append("categoryIds", id));
    }
    try {
        const responseGet = await fetch(`/api/Products/?${params}`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }
        });
        if (!responseGet.ok)
            alert("Eror,please try again")
        else {
            drawProducts(await responseGet.json());
        }
    }
    catch (error) {
        console.log(error)
    }

}

const getCategories = async () => {
    try {
        const responseGet = await fetch(`/api/Categories/`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }
        });
        if (!responseGet.ok)
            alert("Eror,please try again")
        else {
            drawCategories(await responseGet.json());
        }
    }
    catch (error) {
        console.log(error)
    }

}

const drawProducts = (productList) => {
    const tempCard = document.querySelector("template#temp-card")
    const productContainer = document.querySelector("#ProductList")
    productContainer.innerHTML = "";
    productList.forEach(product => {
        const clone = tempCard.content.cloneNode(true);
        clone.querySelector(".img-w").querySelector("img").src = `${product.imageUrl}`
        clone.querySelector("h1").textContent = product.productName
        clone.querySelector(".price").textContent = product.price
        clone.querySelector("button").addEventListener("click", (e) => {addToCart(product)})
        productContainer.appendChild(clone)

    })
}

const drawCategories = (categoryList) => {
    const tempCategory = document.querySelector("template#temp-category")
    const categoryContainer = document.querySelector("#categoryList")
    categoryList.forEach(category => {
        const categoryItem = tempCategory.content.cloneNode(true);
        categoryItem.querySelector(".opt").addEventListener("change", (e) => {
            e.target.checked ? addCategoryId(category.categoryId) : removeCategoryId(category.categoryId)
        })
        categoryItem.querySelector("label").querySelector(".OptionName").textContent = category.categoryName
        categoryContainer.appendChild(categoryItem)  

    })
}

const addCategoryId = (categoryId) => {
    categoryIds.push(categoryId);
    paramObject.categoryIds = categoryIds;
    getProducts();
}

const removeCategoryId = (categoryId) => {
    categoryIds=categoryIds.filter(category => category != categoryId)
    paramObject.categoryIds = categoryIds;
    getProducts();

}

const filterProducts = () => {
    paramObject.minPrice = document.querySelector("#minPrice").value;
    paramObject.maxPrice = document.querySelector("#maxPrice").value;
    paramObject.desc = document.querySelector("#nameSearch").value;
    getProducts();
}

const addToCart = (product) => {
    const cartItems = JSON.parse(sessionStorage.getItem("cartItems") || "[]");
    productIndex = cartItems.findIndex(item => item.productId === product.productId)

    if (productIndex !== -1)
        cartItems[productIndex].quantity++
    else { 
        product.quantity = 1
        cartItems.push(product)
    }

    let bagSum = 0
    cartItems.forEach(item => {
        bagSum += item.quantity
    })
    document.querySelector("#ItemsCountText").textContent = bagSum

    sessionStorage.setItem("cartItems", JSON.stringify(cartItems));
}