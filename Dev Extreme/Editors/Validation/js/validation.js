$(() => {
    $("#registration-form").dxForm({
        formData: {
            name: "",
            email: "",
            password: "",
            confirmPassword: ""
        },
        validationGroup: "registrationGroup",
        items: [{
            dataField: "name",
            label: {
                text: "Name"
            },
            editorType: "dxTextBox",
            validationRules: [{
                type: "required",
                message: "Name is required"
            }]
        }, {
            dataField: "email",
            label: {
                text: "Email"
            },
            editorType: "dxTextBox",
            editorOptions: {
                mode: "email"
            },
            validationRules: [{
                type: "required",
                message: "Email is required"
            }, {
                type: "email",
                message: "Email is invalid"
            }]
        }, {
            dataField: "password",
            label: {
                text: "Password"
            },
            editorType: "dxTextBox",
            editorOptions: {
                mode: "password"
            },
            validationRules: [{
                type: "required",
                message: "Password is required"
            }, {
                type: "pattern",
                pattern: "^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{8,}$",
                message: "Password must be minimum 8 characters, at least one letter and one number"
            }]
        }, {
            dataField: "confirmPassword",
            label: {
                text: "Confirm Password"
            },
            editorType: "dxTextBox",
            editorOptions: {
                mode: "password"
            },
            validationRules: [{
                type: "required",
                message: "Confirm Password is required"
            }, {
                type: "compare",
                comparisonTarget: function () {
                    return $("#registration-form").dxForm("instance").option("formData").password;
                },
                message: "Passwords do not match"
            }]
        }, {
            itemType: "button",
            buttonOptions: {
                text: "Register",
                type: "success",
                useSubmitBehavior: true,
                onClick: function () {
                    var result = DevExpress.validationEngine.validateGroup("registrationGroup");
                    if (result.isValid) {
                        alert("Registration Successful!");
                    }
                }
            }
        }]
    });

    $("#registration-form").dxForm({
        onInitialized: function (e) {
            console.log("Form Initialized");
        },
        onDisposing: function (e) {
            console.log("Form Disposing");
        },
        onOptionChanged: function (e) {
            console.log("Option Changed", e);
        },
        onValidated: function (e) {
            console.log("Form Validated", e);
        }
    });
});