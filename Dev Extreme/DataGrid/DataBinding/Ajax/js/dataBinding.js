$(() => {
    const PAGE_SIZE = 10; // Number of items per page

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
                const totalTasks = parseInt(jqXHR.getResponseHeader('X-Total-Count')); // Get total count from response header
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

    // Initialize DataGrid
    $("#dataGrid").dxDataGrid({
        dataSource: customStore,
        remoteOperations: {
            paging: true
        },
        paging: {
            pageSize: PAGE_SIZE
        },
        pager: {
            showPageSizeSelector: true,
            allowedPageSizes: [5, 10, 20],
            showInfo: true
        },
        columns: [
            { dataField: "id", caption: "ID", width: 50 },
            { dataField: "title", caption: "Task Title" },
            { dataField: "completed", caption: "Completed", dataType: "boolean" }
        ],
        editing: {
            mode: 'popup',
            allowAdding: true,
            allowUpdating: true,
            allowDeleting: true
        },
        onRowInserted: function (e) {
            DevExpress.ui.notify(`Task added successfully`, 'success', 2000);
        },
        onRowUpdated: function (e) {
            DevExpress.ui.notify(`Task updated successfully`, 'success', 2000);
        },
        onRowRemoved: function (e) {
            DevExpress.ui.notify(`Task deleted successfully`, 'success', 2000);
        },

        accessKey: "d",
        activeStateEnabled: true,
        allowColumnReordering: true,
        allowColumnResizing: true,
        autoNavigateToFocusedRow: true,
        cacheEnabled: true,
        cellHintEnabled: true,
        columnAutoWidth: true,
        columnChooser: {
            enabled: true,
            mode: "dragAndDrop"
        },
        columnFixing: {
            enabled: true
        },
        columnHidingEnabled: true,
        columnMinWidth: 50,
        columnResizingMode: "nextColumn",
        customizeColumns: function (columns) {
            columns[1].width = 150;
        },
        dateSerializationFormat: "yyyy-MM-ddTHH:mm:ssZ",
        disabled: false,
        elementAttr: {
            id: "myDataGrid",
            class: "my-custom-class"
        },
        errorRowEnabled: true,
        export: {
            enabled: true,
            fileName: "Tasks",
            excelFilterEnabled: true,
            allowExportSelectedData: true
        },
        filterBuilder: {
            visible: true
        },
        filterBuilderPopup: {
            position: { my: 'center', at: 'center', of: window }
        },
        filterPanel: {
            visible: true
        },
        filterRow: {
            visible: true,
            applyFilter: "auto"
        },
        filterSyncEnabled: true,
        filterValue: [
            ["completed", "=", false]
        ],
        focusedColumnIndex: 1,
        focusedRowEnabled: true,
        focusedRowIndex: 0,
        focusedRowKey: 1,
        focusStateEnabled: true,
        grouping: {
            autoExpandAll: true
        },
        groupPanel: {
            visible: true
        },
        headerFilter: {
            visible: true
        },
        height: 600,
        highlightChanges: true,
        hint: "Tasks Data Grid",
        hoverStateEnabled: true,
        keyboardNavigation: {
            enabled: true
        },
        keyExpr: "id",
        loadPanel: {
            enabled: true
        },
        masterDetail: {
            enabled: false
        },
        noDataText: "No tasks to display",
        onAdaptiveDetailRowPreparing: function (e) {
            console.log("Adaptive detail row is being prepared");
        },
        onCellClick: function (e) {
            console.log("Cell clicked:", e);
        },
        onCellDblClick: function (e) {
            console.log("Cell double-clicked:", e);
        },
        onCellHoverChanged: function (e) {
            console.log("Cell hover changed:", e);
        },
        onCellPrepared: function (e) {
            console.log("Cell prepared:", e);
        },
        onContentReady: function (e) {
            console.log("Content is ready");
        },
        onContextMenuPreparing: function (e) {
            console.log("Context menu preparing:", e);
        },
        onDataErrorOccurred: function (e) {
            console.error("Data error occurred:", e);
        },
        onDisposing: function (e) {
            console.log("Disposing DataGrid");
        },
        onEditCanceled: function (e) {
            console.log("Edit canceled");
        },
        onEditCanceling: function (e) {
            console.log("Edit canceling");
        },
        onEditingStart: function (e) {
            console.log("Editing started");
        },
        onEditorPrepared: function (e) {
            console.log("Editor prepared");
        },
        onEditorPreparing: function (e) {
            console.log("Editor preparing");
        },
        onExporting: function (e) {
            console.log("Exporting data");
        },
        onFocusedCellChanged: function (e) {
            console.log("Focused cell changed");
        },
        onFocusedCellChanging: function (e) {
            console.log("Focused cell changing");
        },
        onFocusedRowChanged: function (e) {
            console.log("Focused row changed");
        },
        onFocusedRowChanging: function (e) {
            console.log("Focused row changing");
        },
        onInitialized: function (e) {
            console.log("DataGrid initialized");
        },
        onInitNewRow: function (e) {
            console.log("Init new row");
        },
        onKeyDown: function (e) {
            console.log("Key down:", e);
        },
        onOptionChanged: function (e) {
            console.log("Option changed:", e);
        },
        onRowClick: function (e) {
            console.log("Row clicked:", e);
        },
        onRowCollapsed: function (e) {
            console.log("Row collapsed:", e);
        },
        onRowCollapsing: function (e) {
            console.log("Row collapsing:", e);
        },
        onRowDblClick: function (e) {
            console.log("Row double-clicked:", e);
        },
        onRowExpanded: function (e) {
            console.log("Row expanded:", e);
        },
        onRowExpanding: function (e) {
            console.log("Row expanding:", e);
        },
        onRowInserting: function (e) {
            console.log("Row inserting");
        },
        onRowPrepared: function (e) {
            console.log("Row prepared");
        },
        onRowRemoving: function (e) {
            console.log("Row removing");
        },
        onRowUpdating: function (e) {
            console.log("Row updating");
        },
        onRowValidating: function (e) {
            console.log("Row validating");
        },
        onSaved: function (e) {
            console.log("Saved changes");
        },
        onSaving: function (e) {
            console.log("Saving changes");
        },
        onSelectionChanged: function (e) {
            console.log("Selection changed");
        },
        onToolbarPreparing: function (e) {
            console.log("Toolbar preparing");
        },
        remoteOperations: {
            paging: true,
            sorting: true,
            filtering: true
        },
        renderAsync: true,
        repaintChangesOnly: true,
        rowAlternationEnabled: true,
        rowDragging: {
            allowReordering: true
        },
        // rowTemplate: function (container, item) {
        //     $("<tr>")
        //         .append($("<td>").text(item.data.title))
        //         .append($("<td>").text(item.data.completed))
        //         .appendTo(container);
        // },
        rtlEnabled: false,
        scrolling: {
            mode: "virtual"
        },
        searchPanel: {
            visible: true,
            highlightCaseSensitive: true
        },
        selectedRowKeys: [],
        selection: {
            mode: "multiple"
        },
        showBorders: true,
        showColumnHeaders: true,
        showColumnLines: true,
        showRowLines: true,
        sortByGroupSummaryInfo: [{
            summaryItem: "count"
        }],
        sorting: {
            mode: "multiple"
        },
        stateStoring: {
            enabled: true,
            type: "localStorage",
            storageKey: "tasksDataGrid"
        },
        summary: {
            totalItems: [{
                column: "id",
                summaryType: "count"
            }]
        },
        tabIndex: 1,
        twoWayBindingEnabled: true,
        visible: true,
        width: "100%",
        wordWrapEnabled: true
    });
});