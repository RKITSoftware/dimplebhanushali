$(document).ready(function () {
    $('#registrationForm').submit(function (event) {
        $('.error-message').text(''); // Clear previous error messages
        var isValid = true; // Flag to track validation status

        // Validate Full Name (should not be empty)
        var fullName = $('#fullName').val();
        if (fullName.trim() === '') {
            $('#fullNameError').text('Full Name is required.');
            isValid = false;
        }

        // Validate Email
        var email = $('#email').val();
        if (!isValidEmail(email)) {
            $('#emailError').text('Please enter a valid email address.');
            isValid = false;
        }

        // Validate Password (minimum length of 8 characters)
        var password = $('#password').val();
        if (password.length < 8) {
            $('#passwordError').text('Password should be at least 8 characters long.');
            isValid = false;
        }

        // Confirm Password (should match Password)
        var confirmPassword = $('#confirmPassword').val();
        if (password !== confirmPassword) {
            $('#confirmPasswordError').text('Password and Confirm Password do not match.');
            isValid = false;
        }

        // Validate Date of Birth (optional additional validation can be added)
        var dateOfBirth = $('#dateOfBirth').val();
        if (!dateOfBirth) {
            $('#dateOfBirthError').text('Date of Birth is required.');
            isValid = false;
        }

        // Validate Gender (should not be "Select your gender")
        var gender = $('#gender').val();
        if (gender === '') {
            $('#genderError').text('Please select your gender.');
            isValid = false;
        }

        // Validate Phone Number (optional additional validation can be added)
        var phoneNumber = $('#phoneNumber').val();
        if (!phoneNumber) {
            $('#phoneNumberError').text('Phone Number is required.');
            isValid = false;
        }

        if (!isValid) {
            event.preventDefault(); // Prevent form submission if validation fails
        } else {
            alert("Registration Successfull!!!");
        }
    });

    // Email validation function
    function isValidEmail(email) {
        // A simple email validation regex (you can use a more robust regex)
        var emailRegex = /^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$/;
        return emailRegex.test(email);
    }
});
