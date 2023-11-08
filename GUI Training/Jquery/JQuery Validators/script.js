$(document).ready(function () {
    $('#registrationForm').validate({
        rules: {
            fullName: {
                required: true
            },
            email: {
                required: true,
                email: true
            },
            password: {
                required: true,
                minlength: 8
            },
            confirmPassword: {
                required: true,
                equalTo: "#password"
            },
            dateOfBirth: {
                required: true
            },
            gender: {
                required: true
            },
            phoneNumber: {
                required: true,
                pattern: /^[6789]\d{9}$/
            }
        },
        messages: {
            fullName: "Full Name is required",
            email: {
                required: "Email is required",
                email: "Please enter a valid email address"
            },
            password: {
                required: "Password is required",
                minlength: "Password should be at least 8 characters long"
            },
            confirmPassword: {
                required: "Confirm Password is required",
                equalTo: "Password and Confirm Password must match"
            },
            dateOfBirth: "Date of Birth is required",
            gender: "Please select your gender",
            phoneNumber: "Phone Number is required"
        },
        errorElement: 'span',
        errorClass: 'text-danger', // CSS class for error messages
        errorPlacement: function (error, element) {
            error.insertAfter(element);
        },
        highlight: function (element, errorClass, validClass) {
            $(element).addClass(errorClass).removeClass(validClass);
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).removeClass(errorClass).addClass(validClass);
        },
        submitHandler: function (form) {
            // The form is valid, show the success alert
            $('#successAlert').show();
        }
    });
});
