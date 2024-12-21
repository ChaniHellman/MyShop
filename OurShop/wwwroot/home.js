

const getDataFromDocument = () => {
    const fullName = document.querySelector("#fullName").value;
    const email = document.querySelector("#email").value;
    const phone = document.querySelector("#phone").value;
    const password = document.querySelector("#password").value;
    return { fullName, email, phone, password }
}

const createUser = async () => {
    const user = getDataFromDocument();
    try {
        const responsePost = await fetch('api/users', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(user)
        });
        if (!responsePost.ok) { 
           
            alert("Error,plese try again")}
        else {
            const dataPost = await responsePost.json();
            alert(`User ${dataPost.fullName} created!`)
        }
        checkPasswordStrength(user.password)
       
    }
    catch (error) {
        console.log(error)
    }
}

const show = () => {
    const signUpDiv = document.getElementById("sign")
    signUpDiv.className = "show"
}

const showUpdate = () => {
    const updatepDiv = document.getElementById("update")
    updatepDiv.className = "show"
}

const getDataFromLogin = () => {
    const email = document.querySelector("#emailLogin").value;
    const password = document.querySelector("#passwordLogin").value;
    return { email, password }
}

const login = async () => {
    const data = getDataFromLogin();
    try {
        const responsePost = await fetch(`api/users/login/?email=${data.email}&password=${data.password}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            query: {
                email: data.email,
                password: data.password
            }
        });
        if (responsePost.status==204)
        alert("User not found")
        if (!responsePost.ok) 
        alert("Eror,please try again")
        else {
            const dataPost = await responsePost.json();
            
        sessionStorage.setItem("id", dataPost.id)
        sessionStorage.setItem("userName", dataPost.fullName)
         alert(`${dataPost.fullName} login `)
         window.location.href = "details.html"
        }
    }
    catch (error) {
        console.log(error)
    }
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
        if (!responsePost.ok)
            alert("Eror,please try again")
        else {
            const dataPut = await responsePut.json();
            alert(`${dataPut.fullName} updated `)
        }
    }
    catch (error) {
        console.log(error)
    }

}
const  checkPasswordStrength = async ( password) => {
    try {
        const passwordStrength = await fetch(`api/users/passwordStrength?password=${password}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            query: {
                password: password
            }
        });
        const p = await passwordStrength.json()
        return p;
       
    }
    catch (error) {
        console.log(error)
    }
}
const fillProgress = async ()=>{
    const progress = document.getElementById("progress")
    const password = document.getElementById("password").value
    const passwordStrength = await checkPasswordStrength(password);
    progress.value = passwordStrength;
   
}