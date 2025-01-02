

const getDataFromDocument = () => {
    const firstName = document.querySelector("#firstName").value;
    const lastName = document.querySelector("#lastName").value;
    const email = document.querySelector("#email").value;
    const password = document.querySelector("#password").value;
    return { firstName, lastName, email, password }
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

            alert("Error,plese try again")
        }
        else {
            const dataPost = await responsePost.json();
            alert(`User ${dataPost.firstName} created!`)
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
        const responsePost = await fetch(`api/Users/login/?email=${data.email}&password=${data.password}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            query: {
                email: data.email,
                password: data.password
            }
        });
        if (responsePost.status == 204)
            alert("User not found")
        if (!responsePost.ok)
            alert("Eror,please try again")
        else {
            const dataPost = await responsePost.json();

            sessionStorage.setItem("UserId", dataPost.userId)
            sessionStorage.setItem("userName", dataPost.firstName)
            alert(`${dataPost.firstName} login `)
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
        const UserId = sessionStorage.getItem("UserId")
        const responsePut = await fetch(`api/users/${UserId}`, {
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
            alert(`${dataPut.firstName} updated `)
        }
    }
    catch (error) {
        console.log(error)
    }

}
const checkPasswordStrength = async (password) => {
    try {
        const passwordStrength = await fetch(`api/users/passwordStrength/?password=${password}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            }
            //query: {
            //    password: password
            //}
        });
        const p = await passwordStrength.json()
        return p;

    }
    catch (error) {
        console.log(error)
    }
}
const fillProgress = async () => {
    const progress = document.getElementById("progress")
    const password = document.getElementById("password").value
    const passwordStrength = await checkPasswordStrength(password);
    progress.value = passwordStrength;

}