// Function to validate email address format
function validateEmail(email) {
    const emailRegex = /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2, 4}$/i;
    return emailRegex.test(email);
}

// Function to display error messages
function displayError(message) {
    const messageElement = document.getElementById("message");
    messageElement.textContent = message;
}


document.getElementById("registerButton").addEventListener("click", function () {
    const firstName = document.getElementById("firstName").value;
    const lastName = document.getElementById("lastName").value;
    const email = document.getElementById("email").value;
    const regUsername = document.getElementById("regUsername").value;
    const regPassword = document.getElementById("regPassword").value;

    if (firstName.trim() === "" || lastName.trim() === "" || email.trim() === "" || regUsername.trim() === "" || regPassword.trim() === "") {
        displayError("All fields are required.");
        return;
    }

    // Save registration data to local storage
    localStorage.setItem(regUsername, JSON.stringify({
        firstName: firstName,
        lastName: lastName,
        email: email,
        password: regPassword
    }));

    document.getElementById("message").textContent = "Registration successful!";
    alert('Registration successful!')
});

document.getElementById("loginButton").addEventListener("click", function () {
    const loginUsername = document.getElementById("loginUsername").value;
    const loginPassword = document.getElementById("loginPassword").value;

    const storedUserData = localStorage.getItem(loginUsername);

    if (storedUserData) {
        const userData = JSON.parse(storedUserData);
        if (userData.password === loginPassword) {
            document.getElementById("message").textContent = "Login successful!";
            alert('Login successful!')
        } else {
            alert('Login failed. Please check your credentials.')
            displayError("Login failed. Please check your credentials.");
        }
    } else {
        alert('Login failed. User not found.')
        displayError("Login failed. User not found.");
    }
});
