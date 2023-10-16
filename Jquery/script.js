$(document).ready(function () {
    // Add a new task to the list
    $('#addTask').click(function () {
        var taskText = $('#taskInput').val();

        if (taskText !== "") {
            $('#taskList').append('<li class="list-group-item d-flex justify-content-between align-items-center">' +
                taskText +
                '<span class="badge badge-danger badge-pill remove-task">X</span>' +
                '</li>');
            $('#taskInput').val("");
        }
    });

    // Remove a task when the X sign is clicked
    $('#taskList').on('click', '.remove-task', function () {
        $(this).closest('li').remove();
    });
});
