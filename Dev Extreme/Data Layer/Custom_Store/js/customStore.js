$(() => {
    const PAGE_SIZE = 10; // Number of items per page
    let currentPage = 0; // Current page index (0-based)
    let totalTasks = 0; // Total number of tasks

    // Define Custom Store
    const customStore = new DevExpress.data.CustomStore({
        key: 'id',

        load: function (loadOptions) {
            const skip = loadOptions.skip || 0; // Default to skip 0 records
            const take = loadOptions.take || PAGE_SIZE; // Default to take PAGE_SIZE records

            // Perform GET request to fetch data with paging
            return $.getJSON('https://jsonplaceholder.typicode.com/todos', {
                _start: skip,
                _limit: take
            }).then(function (response, textStatus, jqXHR) {
                totalTasks = parseInt(jqXHR.getResponseHeader('X-Total-Count')); // Get total count from response header
                return {
                    data: response, // Data to display
                    totalCount: totalTasks // Total count of items (from CustomStore)
                };
            });
        },
        insert: function (values) {
            // Perform POST request to add new item
            return $.post('https://jsonplaceholder.typicode.com/todos', values);
        },
        update: function (key, values) {
            // Perform PUT request to update existing item
            return $.ajax({
                url: `https://jsonplaceholder.typicode.com/todos/${key}`,
                method: 'PUT',
                data: values
            });
        },
        remove: function (key) {
            // Perform DELETE request to delete item
            return $.ajax({
                url: `https://jsonplaceholder.typicode.com/todos/${key}`,
                method: 'DELETE'
            });
        }
    });

    // Function to render tasks in the table
    function renderTasks(tasks) {
        const $taskList = $('#taskList tbody');
        $taskList.empty();
        tasks.forEach(function (task) {
            const row = `<tr data-id="${task.id}">
                            <td>${task.title}</td>
                            <td>
                                <button class="edit" data-id="${task.id}">Edit</button>
                                <button class="delete" data-id="${task.id}">Delete</button>
                            </td>
                        </tr>`;
            $taskList.append(row);
        });
    }

    // Function to load tasks with paging
    function loadTasks() {
        const skip = currentPage * PAGE_SIZE;
        customStore.load({ skip: skip, take: PAGE_SIZE })
            .done(function (result) {
                renderTasks(result.data);
                updatePaginationUI();
            });
    }

    // Function to update pagination UI
    function updatePaginationUI() {
        const totalPages = Math.ceil(totalTasks / PAGE_SIZE);
        $('#prevPage').prop('disabled', currentPage === 0);
        $('#nextPage').prop('disabled', currentPage >= totalPages - 1);
    }

    // Initial load of tasks with default paging (first page, PAGE_SIZE items)
    loadTasks();

    // Function to update `totalTasks` after any modification (edit/delete)
    function updateTotalTasks() {
        // Perform a quick request to get the latest total count
        $.getJSON('https://jsonplaceholder.typicode.com/todos', {
            _start: 0,
            _limit: 1 // Fetch only one record to get the updated total count
        }).then(function (response, textStatus, jqXHR) {
            totalTasks = parseInt(jqXHR.getResponseHeader('X-Total-Count'));
            updatePaginationUI(); // Update pagination UI with the new total count
        });
    }

    // Add Task button click event
    $('#addTask').click(function () {
        const newTaskTitle = $('#taskTitle').val();
        if (newTaskTitle.trim() === '') return;

        customStore.insert({ title: newTaskTitle, completed: false })
            .done(function () {
                $('#taskTitle').val('');
                // Reload tasks after insertion
                loadTasks();
            });
    });

    // Edit button click event
    $('#taskList').on('click', '.edit', function () {
        const taskId = $(this).data('id');
        const newTitle = prompt('Enter new title for task:');
        if (newTitle !== null) {
            customStore.update(taskId, { title: newTitle })
                .done(function () {
                    $(`tr[data-id="${taskId}"] td:first-child`).text(newTitle);
                    DevExpress.ui.notify(`Task with ID ${taskId} updated successfully`, 'success', 2000);
                    updateTotalTasks(); // Update totalTasks after successful update
                });
        }
    });

    // Delete button click event
    $('#taskList').on('click', '.delete', function () {
        const taskId = $(this).data('id');
        customStore.remove(taskId)
            .done(function () {
                $(`tr[data-id="${taskId}"]`).remove();
                DevExpress.ui.notify(`Task with ID ${taskId} deleted successfully`, 'success', 2000);
                updateTotalTasks(); // Update totalTasks after successful deletion
            });
    });

    // Previous page button click event
    $('#prevPage').click(function () {
        if (currentPage > 0) {
            currentPage--;
            loadTasks();
        }
    });

    // Next page button click event
    $('#nextPage').click(function () {
        const totalPages = Math.ceil(totalTasks / PAGE_SIZE);
        if (currentPage < totalPages - 1) {
            currentPage++;
            loadTasks();
        }
    });
});
