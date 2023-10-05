// Define the Student class to represent student data
class Student {
    constructor(name, age, grade, subject, country) {
        this.name = name;
        this.age = age;
        this.grade = grade;
        this.subject = subject;
        this.country = country;
    }
}


// Define the UI class for user interface operations
class UI {

    // Method to add a student to the student list

    addStudentToList(student) {
        const list = document.getElementById('student-list');
        const row = document.createElement('tr');

        // Populate the table row with student data, including the new "Subject" and "Country" fields
        row.innerHTML = `
            <td>${student.name}</td>
            <td>${student.age}</td>
            <td>${student.grade}</td>
            <td>${student.subject}</td>
            <td>${student.country}</td>
            <td><a href="#" class="delete">X</a></td>
        `;

        // Append the row to the student list table
        list.appendChild(row);
    }


    // Method to display an alert message
    showAlert(message, className) {
        const div = document.createElement('div');

        // Add the specified class to the alert div
        div.className = `alert alert-${className}`;
        div.appendChild(document.createTextNode(message));
        const container = document.querySelector('.container');
        const form = document.querySelector('#student-form');

        // Insert the alert before the form in the container
        container.insertBefore(div, form);

        // Remove the alert after 3 seconds (3000 milliseconds)
        setTimeout(function () {
            document.querySelector('.alert').remove();
        }, 3000);

    }


    // Method to delete a student from the list
    deleteStudent(target) {
        if (target.className === 'delete') {
            target.parentElement.parentElement.remove();
        }
    }


    // Method to clear input fields after adding a student
    clearFields() {

        document.getElementById('name').value = '';
        document.getElementById('age').value = '';
        document.getElementById('grade').value = '';
        document.getElementById('subject').value = '';
        document.getElementById('country').value = '';

    }
}


// Event Listening for form submission
document.getElementById('student-form').addEventListener('submit', function (e) {

    // Get values from the form fields
    const name = document.getElementById('name').value;
    const age = document.getElementById('age').value;
    const grade = document.getElementById('grade').value;
    const subject = document.getElementById('subject').value;
    const country = document.getElementById('country').value;

    // Create a new Student object with the input data
    const student = new Student(name, age, grade, subject, country);
    const ui = new UI();

    if (name === '' || age === '' || grade === '' || subject === '' || country === '') {
        // Display an error alert if any field is empty
        ui.showAlert('Please fill in all fields', 'error');

    } else {
        // Add the student to the list, display a success alert, and clear fields
        ui.addStudentToList(student);
        ui.showAlert('Student Added', 'success');
        ui.clearFields();
    }

    // Prevent the form from submitting and refreshing the page
    e.preventDefault();

});


// Event listening for delete operation
document.getElementById('student-list').addEventListener('click', function (e) {
    const ui = new UI();
    // Delete the student when the "X" link is clicked
    ui.deleteStudent(e.target);
    // Display a success alert for the removal
    ui.showAlert('Student Removed!', 'success');
    e.preventDefault();
});
