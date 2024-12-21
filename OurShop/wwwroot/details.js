const welcomeText = () => {
    const welcomeText = document.getElementById("welcome")
    welcomeText.textContent = `Hi ${sessionStorage.getItem("userName")},you'v connected successfuly! lets dive in...`
}
welcomeText();
const getDataFromDocument = () => {
    const fullName = document.querySelector("#fullName").value;
    const email = document.querySelector("#email").value;
    const phone = document.querySelector("#phone").value;
    const password = document.querySelector("#password").value;
    return { fullName, email, phone, password }
}

const showUpdate = () => {
    const updatepDiv = document.getElementById("update")
    updatepDiv.className = "show"
}

const updateUser = async () => {
   const user = getDataFromDocument();
    try {
        const id = sessionStorage.getItem("id")
        const responsePut = await fetch(`api/users/${id}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(user)
        });
       // if (responsePut.status == 204) 
        // alert("User nor found")
        if (!responsePut.ok) { 
        const dataPut = await responsePut.json();
            alert(`User ${dataPut.FullName} updated successfuly` )}
        
        
    }
    catch (error) {
        console.log(error)
    }
}

 