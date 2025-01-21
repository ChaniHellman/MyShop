
const API_URL = '/api/users';

const writeWelcomeText = () => {
    const welcomeText = document.getElementById("welcome")
    welcomeText.textContent = `Hi ${sessionStorage.getItem("userName")}, you have connected successfuly! lets dive in...`
}

writeWelcomeText();

const showUpdate = async () => {
    await fillUpdateInputs()
    const updateDiv = document.getElementById("update")
    updateDiv.className = "show"
}

const fillUpdateInputs = async () => {
    try {
        const id = sessionStorage.getItem("UserId")
        const response = await fetch(`${API_URL}/${id}`, {
            method: 'GET'
        });

        if (response.ok) {
            const userData = await response.json();
            document.querySelector("#firstName").value = userData.firstName;
            document.querySelector("#lastName").value = userData.lastName;
            document.querySelector("#email").value = userData.email;
        }
    }
    catch (error) {
        console.log(error)
    }
}

const getUpdateInputs = () => {
    const firstName = document.querySelector("#firstName").value;
    const lastName = document.querySelector("#lastName").value;
    const email = document.querySelector("#email").value;
    const password = document.querySelector("#password").value;
    return { firstName, lastName, email, password }
}

const updateUser = async () => {

    const userForUpdate = getUpdateInputs();

    try {
        const id = sessionStorage.getItem("UserId")
        const response = await fetch(`${API_URL}/${id}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(userForUpdate)
        });
        
        if (response.ok) {
            const updatedUser = await response.json();
            alert(`User ${updatedUser.firstName} updated successfully`);
        }
    }
    catch (error) {
        console.log(error)
    }
}

