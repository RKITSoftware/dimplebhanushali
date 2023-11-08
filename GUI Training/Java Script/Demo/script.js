const form = document.getElementById('form');
const username = document.getElementById('username');
const email = document.getElementById('email');
const phone = document.getElementById('phone');
const password = document.getElementById('password');
const password2 = document.getElementById('password2');

// adding event listener to Submit Button
form.addEventListener('submit', e => {
    e.preventDefault();

    checkInputs();
});

var flag = 0
// Checking All Elements
function checkInputs() {
    // trim to remove the whitespaces
    const usernameValue = username.value.trim();
    const emailValue = email.value.trim();
    const phoneValue = phone.value.trim();
    const passwordValue = password.value.trim();
    const password2Value = password2.value.trim();

    // Username Checking
    if (usernameValue === '') {
        setErrorFor(username, 'Username cannot be blank');
        flag += 1
    } else {
        setSuccessFor(username);
    }

    // Email Checking
    if (emailValue === '') {
        setErrorFor(email, 'Email cannot be blank');
        flag += 1
    } else if (!isEmail(emailValue)) {
        setErrorFor(email, 'Not a valid email');
        flag += 1
    } else {
        setSuccessFor(email);
    }

    // Phone Number Checking
    if (phoneValue == '') {
        setErrorFor(phone, 'Phone Number is Required')
        flag += 1
    } else {
        setSuccessFor(phone)
    }

    // Password Checking
    if (passwordValue === '') {
        setErrorFor(password, 'Password cannot be blank');
        flag += 1
    } else {
        setSuccessFor(password);
    }

    // Confirm Password Checking
    if (password2Value === '') {
        setErrorFor(password2, 'Password2 cannot be blank');
        flag += 1
    } else if (passwordValue !== password2Value) {
        setErrorFor(password2, 'Passwords does not match');
        flag += 1
    } else {
        setSuccessFor(password2);
    }
    if (flag == 0) {
        success()
    }

}

// Form Validate Successfull
function success() {
    alert('Form Submitted Successfully')
}

// Error Msg Showing
function setErrorFor(input, message) {
    const formControl = input.parentElement;
    const small = formControl.querySelector('small');
    formControl.className = 'form-control error';
    small.innerText = message;
}


// Successs Outline for Success
function setSuccessFor(input) {
    const formControl = input.parentElement;
    formControl.className = 'form-control success';
}

// Email Validation
function isEmail(email) {
    return /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/.test(email);
}