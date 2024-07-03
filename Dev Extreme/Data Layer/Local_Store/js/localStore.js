$(() => {
    const PAGE_SIZE = 10;
    let currentPage = 0;
    let totalTasks = 0;

    // Sample data for local store
    const initialData = [
        { id: 1, title: 'Task 1', completed: false },
        { id: 2, title: 'Task 2', completed: true },
        { id: 3, title: 'Task 3', completed: false },
        { id: 4, title: 'Task 4', completed: true },
        { id: 5, title: 'Task 5', completed: false },
        { id: 6, title: 'Task 6', completed: true },
        { id: 7, title: 'Task 7', completed: false },
        { id: 8, title: 'Task 8', completed: true },
        { id: 9, title: 'Task 9', completed: false },
        { id: 10, title: 'Task 10', completed: true },
        { id: 11, title: 'Task 11', completed: false },
        { id: 12, title: 'Task 12', completed: true }
    ];

    // Maintain local data
    let localData = initialData.slice();

    // Define Local Store
    const localStore = new DevExpress.data.CustomStore({
        key: 'id',
        load: function (loadOptions) {
            const skip = loadOptions.skip || 0;
            const take = loadOptions.take || PAGE_SIZE;

            return new Promise((resolve) => {
                const tasks = localData.slice(skip, skip + take);
                totalTasks = localData.length;
                resolve({
                    data: tasks,
                    totalCount: totalTasks
                });
            });
        },
        insert: function (values) {
            return new Promise((resolve) => {
                const newId = localData.length ? Math.max(localData.map(task => task.id)) + 1 : 1;
                const newTask = { id: newId, ...values };
                localData.push(newTask);
                resolve(newTask);
            });
        },
        update: function (key, values) {
            return new Promise((resolve) => {
                const taskIndex = localData.findIndex(task => task.id === key);
                if (taskIndex > -1) {
                    localData[taskIndex] = { ...localData[taskIndex], ...values };
                    resolve(localData[taskIndex]);
                }
            });
        },
        remove: function (key) {
            return new Promise((resolve) => {
                localData = localData.filter(task => task.id !== key);
                resolve();
            });
        },
        errorHandler: function (error) {
            // Handle errors here
            console.error('Local Store Error:', error.message);
        }
    });

    // Function to render tasks in the table
    function renderTasks(tasks) {
        const $taskList = $('#taskList tbody');
        $taskList.empty();
        tasks.forEach(function (task) {
            const row = `<tr data-id="${task.id}">
                            <td>${task.title}</td>
                            <td>${task.completed ? 'Completed' : 'Incomplete'}</td>
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
        localStore.load({ skip: skip, take: PAGE_SIZE })
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

    // Add Task button click event
    $('#addTask').click(function () {
        const newTaskTitle = $('#taskTitle').val();
        if (newTaskTitle.trim() === '') return;

        localStore.insert({ title: newTaskTitle, completed: false })
            .done(function () {
                $('#taskTitle').val('');
                // Reload tasks after insertion
                loadTasks();
                DevExpress.ui.notify(`Task Added successfully`, 'success', 2000);
            });
    });

    // Edit button click event
    $('#taskList').on('click', '.edit', function () {
        const taskId = $(this).data('id');
        const task = localData.find(t => t.id === taskId);
        const newTitle = prompt('Enter new title for task:', task.title);

        if (newTitle !== null) {
            localStore.update(taskId, { title: newTitle })
                .done(function () {
                    // Reload tasks after update
                    loadTasks();
                    DevExpress.ui.notify(`Task with ID ${taskId} updated successfully`, 'success', 2000);
                });
        }
    });

    // Delete button click event
    $('#taskList').on('click', '.delete', function () {
        const taskId = $(this).data('id');
        localStore.remove(taskId)
            .done(function () {
                // Reload tasks after deletion
                loadTasks();
                DevExpress.ui.notify(`Task with ID ${taskId} Deleted successfully`, 'success', 2000);
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
