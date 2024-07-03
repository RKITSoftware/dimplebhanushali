$(() => {
    const resumeData = [{
        UserDetails: {
            FirstName: '',
            LastName: '',
            Email: '',
            Mobile: '',
            Age: null,
            ProfessionalTitle: ''
        },
        education: [],
        workExperience: [],
        projects: [],
        certificates: [],
        knownLanguages: [],
        skills: []
    }];

    const sendRequest = function (value) {
        const invalidEmail = 'test@dx-email.com';
        const d = $.Deferred();
        setTimeout(() => {
            d.resolve(value !== invalidEmail);
        }, 1000);
        return d.promise();
    };

    $('#firstName').dxTextBox({
        value: resumeData[0].UserDetails.FirstName,
        placeholder: 'Enter First Name',
        elementAttr: {
            id: 'firstName'
        },
        onValueChanged: function (e) {
            resumeData[0].UserDetails.FirstName = e.value;

        }
    }).dxValidator({
        validationRules: [{
            type: 'required',
            message: 'First Name is required'
        },
        {
            type: 'pattern',
            pattern: /^[^0-9]+$/,
            message: 'Do not use digits in the First Name.'
        },
        {
            type: 'stringLength',
            min: 5,
            message: 'First Name must have at least 5 characters'
        }
        ]
    });

    $('#lastName').dxTextBox({
        value: resumeData[0].UserDetails.LastName,
        placeholder: 'Enter Last Name',
        elementAttr: {
            id: 'lastName'
        },
        onValueChanged: function (e) {
            resumeData[0].UserDetails.LastName = e.value;

        }
    }).dxValidator({
        validationRules: [{
            type: 'required',
            message: 'Last Name is required'
        },
        {
            type: 'pattern',
            pattern: /^[^0-9]+$/,
            message: 'Do not use digits in the Last Name.'
        },
        {
            type: 'stringLength',
            min: 3,
            message: 'Last Name must have at least 3 characters'
        }
        ]
    });

    $('#email').dxTextBox({
        value: resumeData[0].UserDetails.Email,
        placeholder: 'Enter Email',
        mode: 'email',
        elementAttr: {
            id: 'email'
        },
        onValueChanged: function (e) {
            resumeData[0].UserDetails.Email = e.value;

        }
    }).dxValidator({
        validationRules: [{
            type: 'required',
            message: 'Email is required'
        },
        {
            type: 'email',
            message: 'Invalid email format'
        },
        {
            type: 'async',
            message: 'Email is already registered',
            validationCallback(params) {
                return sendRequest(params.value);
            }
        }
        ]
    });

    $('#mobile').dxTextBox({
        value: resumeData[0].UserDetails.Mobile,
        placeholder: 'Enter Mobile',
        mask: "+\\91 X0000 00000",
        maskChar: "_",
        maskInvalidMessage: "The phone number must follow the pattern: +91 X0000 00000",
        maskRules: {
            "X": /[6-9]/
        },
        elementAttr: {
            id: 'mobile'
        },
        onValueChanged: function (e) {
            resumeData[0].UserDetails.Mobile = e.value;

        }
    }).dxValidator({
        validationRules: [{
            type: 'required',
            message: 'Mobile is required'
        },
        {
            type: 'pattern',
            pattern: /^[0-9]{10}$/,
            message: 'Mobile number must be 10 digits'
        },
        {
            type: 'async',
            validationCallback(params) {
                return sendRequest(params.value);
            }
        }
        ]
    });

    $('#age').dxNumberBox({
        value: resumeData[0].UserDetails.Age,
        placeholder: 'Enter Age',
        showSpinButtons: true,
        min: 18,
        max: 100,
        elementAttr: {
            id: 'age'
        },
        onValueChanged: function (e) {
            resumeData[0].UserDetails.Age = e.value;

        }
    }).dxValidator({
        validationRules: [{
            type: 'required',
            message: 'Age is required'
        },
        {
            type: 'range',
            min: 18,
            max: 100,
            message: 'Age must be between 18 and 100'
        }
        ]
    });

    $('#professionalTitle').dxTextBox({
        value: resumeData[0].UserDetails.ProfessionalTitle,
        placeholder: 'Enter Professional Title',
        elementAttr: {
            id: 'professionalTitle'
        },
        onValueChanged: function (e) {
            resumeData[0].UserDetails.ProfessionalTitle = e.value;

        }
    }).dxValidator({
        validationRules: [{
            type: 'required',
            message: 'Professional Title is required'
        }]
    });

    $('#genderRadio').dxRadioGroup({
        items: [{
            text: 'Male',
            value: 'male'
        },
        {
            text: 'Female',
            value: 'female'
        },
        {
            text: 'Other',
            value: 'other'
        }
        ],
        value: 'male', // Initial selected value
        layout: 'horizontal', // Display options horizontally
        elementAttr: {
            id: 'genderRadio'
        },
        onValueChanged: function (e) {
            resumeData[0].UserDetails.Gender = e.value;

        }
    }).dxValidator({
        validationRules: [{
            type: 'required',
            message: 'Gender is required'
        }]
    });

    $('#dobDateBox').dxDateBox({
        placeholder: '12/31/2018, 2:52 PM',
        type: 'date',
        showClearButton: true,
        useMaskBehavior: true,
        placeholder: 'Select Date of Birth', // Placeholder text
        elementAttr: {
            id: 'dobDateBox'
        },
        onValueChanged: function (e) {
            resumeData[0].UserDetails.DateOfBirth = e.value;

        }
    }).dxValidator({
        validationRules: [{
            type: 'required',
            message: 'Date of birth is required'
        },
        {
            type: 'custom',
            validationCallback(params) {
                const selectedDate = params.value;
                const today = new Date();
                const maxDate = new Date();
                maxDate.setFullYear(maxDate.getFullYear() - 15); // Must be at least 15 years old
                return selectedDate <= maxDate;
            },
            message: 'You must be at least 15 years old'
        }
        ]
    });

    $('#educationGrid').dxDataGrid({
        dataSource: resumeData[0].education,
        columns: [
            { dataField: 'institute', caption: 'Institute' },
            { dataField: 'degree', caption: 'Degree' },
            { dataField: 'fieldOfStudy', caption: 'Field of Study' },
            { dataField: 'educationYear', caption: 'Year' }
        ],
        editing: {
            mode: 'popup',
            allowUpdating: true,
            allowAdding: true,
            allowDeleting: true
        },
        onRowInserted: function (e) {
            resumeData[0].education.push(e.data);
        },
        onRowUpdated: function (e) {
            const index = resumeData[0].education.findIndex(item => item === e.oldData);
            resumeData[0].education[index] = e.data;
        },
        onRowRemoved: function (e) {
            const index = resumeData[0].education.findIndex(item => item === e.data);
            resumeData[0].education.splice(index, 1);
        }
    });

    $('#workExperienceGrid').dxDataGrid({
        dataSource: resumeData[0].workExperience,
        columns: [
            { dataField: 'company', caption: 'Company' },
            { dataField: 'position', caption: 'Position' },
            { dataField: 'startDate', caption: 'Start Date', dataType: 'date' },
            { dataField: 'endDate', caption: 'End Date', dataType: 'date' },
            { dataField: 'responsibilities', caption: 'Responsibilities' }
        ],
        editing: {
            mode: 'popup',
            allowUpdating: true,
            allowAdding: true,
            allowDeleting: true
        },
        onRowInserted: function (e) {
            resumeData[0].workExperience.push(e.data);

        },
        onRowUpdated: function (e) {
            const index = resumeData[0].workExperience.findIndex(item => item === e.oldData);
            resumeData[0].workExperience[index] = e.data;

        },
        onRowRemoved: function (e) {
            const index = resumeData[0].workExperience.findIndex(item => item === e.data);
            resumeData[0].workExperience.splice(index, 1);

        }
    });

    $('#projectsGrid').dxDataGrid({
        dataSource: resumeData[0].projects,
        columns: [
            { dataField: 'projectName', caption: 'Project Name' },
            { dataField: 'description', caption: 'Description' },
            { dataField: 'startDate', caption: 'Start Date', dataType: 'date' },
            { dataField: 'endDate', caption: 'End Date', dataType: 'date' }
        ],
        editing: {
            mode: 'popup',
            allowUpdating: true,
            allowAdding: true,
            allowDeleting: true
        },
        onRowInserted: function (e) {
            resumeData[0].projects.push(e.data);

        },
        onRowUpdated: function (e) {
            const index = resumeData[0].projects.findIndex(item => item === e.oldData);
            resumeData[0].projects[index] = e.data;

        },
        onRowRemoved: function (e) {
            const index = resumeData[0].projects.findIndex(item => item === e.data);
            resumeData[0].projects.splice(index, 1);

        }
    });

    $('#certificatesGrid').dxDataGrid({
        dataSource: resumeData[0].certificates,
        columns: [
            { dataField: 'certificateName', caption: 'Certificate Name' },
            { dataField: 'organization', caption: 'Organization' }
        ],
        editing: {
            mode: 'popup',
            allowUpdating: true,
            allowAdding: true,
            allowDeleting: true
        },
        onRowInserted: function (e) {
            resumeData[0].certificates.push(e.data);

        },
        onRowUpdated: function (e) {
            const index = resumeData[0].certificates.findIndex(item => item === e.oldData);
            resumeData[0].certificates[index] = e.data;

        },
        onRowRemoved: function (e) {
            const index = resumeData[0].certificates.findIndex(item => item === e.data);
            resumeData[0].certificates.splice(index, 1);

        }
    });

    $('#knownLanguages').dxTagBox({
        dataSource: ['English', 'Spanish', 'French', 'Gujarati', 'Hindi'],
        value: [],
        placeholder: 'Select Languages',
        showClearButton: true,
        searchEnabled: true,
        multiline: false,
        onValueChanged: function (e) {
            resumeData[0].knownLanguages = e.value;

        }
    });

    $('#skills').dxTagBox({
        dataSource: ['JavaScript', 'HTML', 'CSS', 'React', 'Angular', 'Vue', 'Node.js'],
        value: [],
        placeholder: 'Select or enter your skills',
        showClearButton: true,
        searchEnabled: true,
        multiline: false,
        onValueChanged: function (e) {
            resumeData[0].skills = e.value;

        }
    });

    $('#fileUploader').dxFileUploader({
        multiple: false,
        accept: 'image/*',
        uploadMode: 'useForm', // Upload mode: 'useButtons' or 'useForm'
        uploadUrl: '/upload', // URL to handle file upload
        onUploaded: function (e) {
            console.log('Profile photo uploaded:', e.request);
        },
        elementAttr: {
            id: 'fileUploader'
        }
    });

    // Initialize DevExtreme CheckBox for Terms and Conditions
    const checkBoxInstance = $('#checkBox').dxCheckBox({
        text: 'I agree to the terms and conditions',
        elementAttr: {
            id: 'checkBox'
        }
    }).dxValidator({
        validationRules: [{
            type: 'required',
            message: 'Please agree to the terms and conditions'
        }],
        validationGroup: 'termsValidation'
    }).dxValidator('instance');

    $('#submitButton').dxButton({
        text: 'Submit',
        useSubmitBehavior: true,
        type: 'success',
        onClick: function () {
            const validationResults = validateAllFields();

            if (validationResults.isValid) {
                submitForm();
            } else {
                showValidationErrors(validationResults.errors);
            }
        }
    });


    function validateAllFields() {
        const validators = [
            $('#firstName').dxValidator('instance'),
            $('#lastName').dxValidator('instance'),
            $('#email').dxValidator('instance'),
            $('#mobile').dxValidator('instance'),
            $('#age').dxValidator('instance'),
            $('#professionalTitle').dxValidator('instance'),
            $('#genderRadio').dxValidator('instance'),
            $('#dobDateBox').dxValidator('instance')
        ];

        let isValid = true;
        const errors = [];

        validators.forEach(validator => {
            if (validator) {
                validator.validate();
                if (!validator.option('isValid')) {
                    isValid = false;
                    errors.push({
                        field: validator.element().attr('id'),
                        message: validator.option('validationError').message
                    });
                }
            }
        });

        return { isValid, errors };
    }

    function submitForm() {
        const resumeDataJSON = JSON.stringify(resumeData[0]);

        // Store data in session storage
        sessionStorage.setItem('resumeData', resumeDataJSON);

        console.log(resumeData);

        // Call POST API with the data list
        postResumeData(resumeData);

        DevExpress.ui.notify('Form Submitted !!!', 'success', 1500);
    }

    function showValidationErrors(errors) {
        const $popup = $('<div>').dxPopup({
            title: 'Validation Errors',
            width: 400,
            height: 'auto',
            visible: true,
            dragEnabled: false,
            closeOnOutsideClick: true,
            contentTemplate: function (contentElement) {
                const $errorList = $('<ul>').appendTo(contentElement);

                errors.forEach(error => {
                    $('<li>', {
                        text: error.message
                    }).appendTo($errorList);
                });
            },
            onHidden: function () {
                $popup.remove();
            }
        });

        $popup.dxPopup('instance').show();
    }

    function postResumeData(dataList) {
        const apiUrl = 'https://localhost:7286/api/CLBulk';

        $.ajax({
            url: apiUrl,
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(dataList),
            xhrFields: {
                responseType: 'blob'  // Ensure the response type is set to blob
            },
            success: function (response, status, xhr) {
                // Check content type of the response
                const contentType = xhr.getResponseHeader('content-type');

                // Determine how to handle the file based on content type
                if (contentType && contentType.includes('application/zip')) {
                    // Handle ZIP file response
                    const blob = new Blob([response], { type: contentType });
                    const url = window.URL.createObjectURL(blob);
                    const a = document.createElement('a');
                    a.style.display = 'none';
                    a.href = url;
                    a.download = 'resume.zip';  // Set the default file name here
                    document.body.appendChild(a);
                    a.click();
                    window.URL.revokeObjectURL(url);
                    DevExpress.ui.notify('ZIP file downloaded successfully', 'success', 1500);
                } else {
                    // Handle other types of responses if needed
                    console.log('Received response:', response);
                    alert('Unexpected response received');
                }
            },
            error: function (xhr, status, error) {
                console.error('Error occurred during POST request');
                console.error(status, error);
            }
        });
    }

});
