﻿const API_URL = '/api/users';

const getUserInputs = () => {
    const firstName = document.querySelector("#firstName").value;
    const lastName = document.querySelector("#lastName").value;
    const email = document.querySelector("#email").value;
    const password = document.querySelector("#password").value;
    return { firstName, lastName, email, password };
}

const validateInputs = ({ firstName, lastName, email, password }) => {
    if (!firstName || !lastName || !email || !password) {
        alert("All fields must be filled out.");
        return false;
    }

    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!emailRegex.test(email)) {
        alert("Please enter a valid email address.");
        return false;
    }

    return true;
}

const createUser = async () => {
    const user = getUserInputs();

    if (!validateInputs(user)) {
        return;
    }

    try {
        const responsePost = await fetch(`${API_URL}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(user)
        });

        if (responsePost.status === 409) {
            alert("Username already taken!");
        } else if (!responsePost.ok) {
            alert("Error, please try again");
        } else {
            const dataPost = await responsePost.json();
            alert(`User ${dataPost.firstName} created!`);
        }

        checkPasswordStrength(user.password);

    } catch (error) {
        console.log(error);
    }
}

const showSignUp = () => {
    const signUpDiv = document.getElementById("sign");
    signUpDiv.className = "show";
}

const showUpdate = () => {
    const updateDiv = document.getElementById("update");
    updateDiv.className = "show";
}

const getLoginInputs = () => {
    const email = document.querySelector("#emailLogin").value;
    const password = document.querySelector("#passwordLogin").value;
    return { email, password };
}

const validateLoginInputs = ({ email, password }) => {
    if (!email || !password) {
        alert("All fields must be filled out.");
        return false;
    }

    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!emailRegex.test(email)) {
        alert("Please enter a valid email address.");
        return false;
    }

    return true;
}

const login = async () => {
    const data = getLoginInputs();

    if (!validateLoginInputs(data)) {
        return;
    }

    try {
        const response = await fetch(`${API_URL}/login`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ email: data.email, password: data.password })
        });

        if (response.status === 204) {
            alert("User not found");
        } else if (!response.ok) {
            alert("Error, please try again");
        } else {
            const userData = await response.json();

            sessionStorage.setItem("UserId", userData.userId);
            sessionStorage.setItem("userName", userData.firstName);

            alert(`${userData.firstName} logged in`);

            const cartItems = JSON.parse(sessionStorage.getItem("cartItems") || "[]");
            if (cartItems.length != 0) {
                window.location.href = "Products.html?fromShoppingBag=1";
            }
            else {
                window.location.href = "Products.html";
            }

        }
    }
    catch (error) {
        console.log(error);
    }
}

const updateUser = async () => {
    const user = getUserInputs();

    if (!validateInputs(user)) {
        return;
    }

    try {
        const UserId = sessionStorage.getItem("UserId");
        const responsePut = await fetch(`${API_URL}/${UserId}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(user)
        });

        if (!responsePut.ok) {
            alert("Error, please try again");
        } else {
            const dataPut = await responsePut.json();
            alert(`${dataPut.firstName} updated`);
        }
    }
    catch (error) {
        console.log(error);
    }
}

const checkPasswordStrength = async (password) => {
    try {
        const strengthResponse = await fetch(`${API_URL}/passwordStrength/?password=${password}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            }
        });

        const passwordStrength = await strengthResponse.json();
        return passwordStrength;
    }
    catch (error) {
        console.log(error);
    }
}

const fillProgress = async () => {
    const progress = document.getElementById("progress");
    const password = document.getElementById("password").value;

    if (!password) {
        progress.value = 0;
        return;
    }

    const passwordStrength = await checkPasswordStrength(password);
    progress.value = passwordStrength;
}