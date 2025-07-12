const API_URL = '/api/users';

const writeWelcomeText = () => {
    const welcomeText = document.getElementById("welcome")
    welcomeText.textContent = `Hi ${sessionStorage.getItem("userName")}, you have connected successfully! Let's dive in...`
}

writeWelcomeText();

const showUpdate = async () => {
    await fillUpdateInputs();
    const updateDiv = document.getElementById("update");
    updateDiv.className = "show";
};

const handleUnauthorized = () => {
    sessionStorage.clear();
    window.location.href = "https://localhost:44368/html/home.html";
};

const fillUpdateInputs = async () => {
    try {
        const id = sessionStorage.getItem("UserId");
        const response = await fetch(`${API_URL}/${id}`, {
            method: 'GET',
            credentials: 'include'
        });

        if (response.status === 401) {
            handleUnauthorized();
            return;
        }

        if (response.ok) {
            const userData = await response.json();
            document.querySelector("#firstName").value = userData.firstName;
            document.querySelector("#lastName").value = userData.lastName;
            document.querySelector("#email").value = userData.email;
        }
    }
    catch (error) {
        alert(error);
        console.log(error);
    }
}

const getUpdateInputs = () => {
    const firstName = document.querySelector("#firstName").value;
    const lastName = document.querySelector("#lastName").value;
    const email = document.querySelector("#email").value;
    const password = document.querySelector("#password").value;
    return { firstName, lastName, email, password }
}

const validateInputs = ({ firstName, lastName, email, password }) => {
    if (!firstName || !lastName || !email || !password) {
        alert("All fields are required.");
        return false;
    }

    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!emailRegex.test(email)) {
        alert("Please enter a valid email address.");
        return false;
    }

    return true;
}

const updateUser = async () => {
    const userForUpdate = getUpdateInputs();

    if (!validateInputs(userForUpdate)) {
        return;
    }

    try {
        const id = sessionStorage.getItem("UserId");
        const response = await fetch(`${API_URL}/${id}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            credentials: 'include',
            body: JSON.stringify(userForUpdate)
        });

        if (response.status === 401) {
            handleUnauthorized();
            return;
        }

        if (response.ok) {
            alert(`User ${userForUpdate.firstName} updated successfully`);
        } else {
            alert("Error, please try again.");
        }
    } catch (error) {
        alert("An error occurred, please try again.");
        console.log(error);
    }
}

const backToHome = () => {
    window.location.href = "Products.html?fromShoppingBag=1"
}