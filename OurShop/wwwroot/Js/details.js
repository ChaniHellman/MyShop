const showUpdate = async () => {
    const updatepDiv = document.getElementById("update")
    updatepDiv.className = "show"
    try {
        const id = sessionStorage.getItem("UserId")
        const responseGet = await fetch(`/api/Users/${id}`, {
            method: 'GET'
        });
        if (responseGet.ok) {
            const dataGet = await responseGet.json();
            document.querySelector("#firstName").value = dataGet.firstName;
            document.querySelector("#lastName").value = dataGet.lastName;
            document.querySelector("#email").value = dataGet.email;
        }
    }
    catch (error) {
        console.log(error)
    }
}

const welcomeText = () => {
    const welcomeText = document.getElementById("welcome")
    welcomeText.textContent = `Hi ${sessionStorage.getItem("userName")},you'v connected successfuly! lets dive in...`
}
welcomeText();
const getDataFromDocument = () => {
    const firstName = document.querySelector("#firstName").value;
    const lastName = document.querySelector("#lastName").value;
    const email = document.querySelector("#email").value;
    const password = document.querySelector("#password").value;
    return { firstName, lastName, email, password }
}



const updateUser = async () => {
    const user = getDataFromDocument();


    try {
        const id = sessionStorage.getItem("UserId")
        const responsePut = await fetch(`/api/users/${id}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(user)
        });
        //if (responsePut.status == 204) 
        // alert("User nor found")
        if (responsePut.ok) {
            const dataPut = await responsePut.json();
            alert(`User ${dataPut.firstName} updated successfully`);
        }
    }
    catch (error) {
        console.log(error)
    }
}

