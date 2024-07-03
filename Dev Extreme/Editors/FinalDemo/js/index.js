$(function () {
    // Initialize the main form
    $("#resume-form").dxForm({
        formData: {
            UserDetails: {},
            Education: [],
            WorkExperience: [],
            Projects: [],
            Certificates: [],
            KnownLanguages: [],
            Skills: []
        },
        items: [
            {
                itemType: "group",
                caption: "User Details",
                items: [
                    { dataField: "UserDetails.FirstName", label: { text: "First Name" }, editorType: "dxTextBox" },
                    { dataField: "UserDetails.LastName", label: { text: "Last Name" }, editorType: "dxTextBox" },
                    { dataField: "UserDetails.Email", label: { text: "Email" }, editorType: "dxTextBox" },
                    { dataField: "UserDetails.Mobile", label: { text: "Mobile" }, editorType: "dxTextBox" },
                    { dataField: "UserDetails.Age", label: { text: "Age" }, editorType: "dxNumberBox" },
                    { dataField: "UserDetails.ProfessionalTitle", label: { text: "Professional Title" }, editorType: "dxTextBox" }
                ]
            },
            {
                itemType: "group",
                caption: "Education",
                items: [
                    {
                        dataField: "Education",
                        label: { text: "Education" },
                        editorType: "dxDataGrid",
                        editorOptions: {
                            dataSource: [], // Placeholder for dataSource
                            columns: [
                                { dataField: "Institute", caption: "Institute", dataType: "string" },
                                { dataField: "Degree", caption: "Degree", dataType: "string" },
                                { dataField: "FieldOfStudy", caption: "Field of Study", dataType: "string" },
                                { dataField: "EducationYear", caption: "Year", dataType: "number" }
                            ],
                            editing: {
                                mode: "form",
                                allowUpdating: true,
                                allowAdding: true,
                                allowDeleting: true
                            }
                        }
                    }
                ]
            },
            {
                itemType: "group",
                caption: "Work Experience",
                items: [
                    {
                        dataField: "WorkExperience",
                        label: { text: "Work Experience" },
                        editorType: "dxDataGrid",
                        editorOptions: {
                            dataSource: [], // Placeholder for dataSource
                            columns: [
                                { dataField: "Company", caption: "Company", dataType: "string" },
                                { dataField: "Position", caption: "Position", dataType: "string" },
                                { dataField: "StartDate", caption: "Start Date", dataType: "date" },
                                { dataField: "EndDate", caption: "End Date", dataType: "date" },
                                { dataField: "Responsibilities", caption: "Responsibilities", dataType: "string" }
                            ],
                            editing: {
                                mode: "form",
                                allowUpdating: true,
                                allowAdding: true,
                                allowDeleting: true
                            }
                        }
                    }
                ]
            },
            {
                itemType: "group",
                caption: "Projects",
                items: [
                    {
                        dataField: "Projects",
                        label: { text: "Projects" },
                        editorType: "dxDataGrid",
                        editorOptions: {
                            dataSource: [], // Placeholder for dataSource
                            columns: [
                                { dataField: "ProjectName", caption: "Project Name", dataType: "string" },
                                { dataField: "Description", caption: "Description", dataType: "string" },
                                { dataField: "StartDate", caption: "Start Date", dataType: "date" },
                                { dataField: "EndDate", caption: "End Date", dataType: "date" }
                            ],
                            editing: {
                                mode: "form",
                                allowUpdating: true,
                                allowAdding: true,
                                allowDeleting: true
                            }
                        }
                    }
                ]
            },
            {
                itemType: "group",
                caption: "Certificates",
                items: [
                    {
                        dataField: "Certificates",
                        label: { text: "Certificates" },
                        editorType: "dxDataGrid",
                        editorOptions: {
                            dataSource: [], // Placeholder for dataSource
                            columns: [
                                { dataField: "CertificateName", caption: "Certificate Name", dataType: "string" },
                                { dataField: "Organization", caption: "Organization", dataType: "string" }
                            ],
                            editing: {
                                mode: "form",
                                allowUpdating: true,
                                allowAdding: true,
                                allowDeleting: true
                            }
                        }
                    }
                ]
            },
            {
                itemType: "group",
                caption: "Languages and Skills",
                items: [
                    {
                        dataField: "KnownLanguages",
                        label: { text: "Known Languages" },
                        editorType: "dxTagBox",
                        editorOptions: {
                            dataSource: ["English", "Spanish", "French", "German", "Chinese", "Japanese"],
                            searchEnabled: true
                        }
                    },
                    {
                        dataField: "Skills",
                        label: { text: "Skills" },
                        editorType: "dxTagBox",
                        editorOptions: {
                            dataSource: ["C#", "Java", "Python", "JavaScript", "HTML", "CSS"],
                            searchEnabled: true
                        }
                    }
                ]
            }
        ]
    });

    // Submit button
    $("#submit-button").dxButton({
        text: "Submit",
        type: "success",
        onClick: function () {
            const formData = $("#resume-form").dxForm("instance").option("formData");
            console.log(formData); // Replace with your submit logic
        }
    });
});
