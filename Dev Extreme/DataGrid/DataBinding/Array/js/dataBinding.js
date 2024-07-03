$(() => {
    const initialData = [
        { id: 1, name: 'MacBook Pro', price: 1299, manufacturer: 'Apple', inStock: true },
        { id: 2, name: 'Wireless Mouse', price: 299, manufacturer: 'Logitech', inStock: true },
        { id: 3, name: 'Mechanical Keyboard', price: 120, manufacturer: 'Corsair', inStock: false },
        { id: 4, name: 'Laptop Charger', price: 75, manufacturer: 'Dell', inStock: true },
        { id: 5, name: 'USB-C Cable', price: 19, manufacturer: 'Anker', inStock: true },
        { id: 6, name: 'External Hard Drive', price: 89, manufacturer: 'Seagate', inStock: false },
        { id: 7, name: 'Noise Cancelling Headphones', price: 299, manufacturer: 'Sony', inStock: true },
        { id: 8, name: 'Smartphone', price: 999, manufacturer: 'Samsung', inStock: true },
        { id: 9, name: 'Tablet', price: 499, manufacturer: 'Apple', inStock: false },
        { id: 10, name: 'Bluetooth Speaker', price: 150, manufacturer: 'Bose', inStock: true }
    ];

    const gridWidget = $('#dataGrid').dxDataGrid({
        dataSource: initialData,
        columns: [
            { dataField: 'id', caption: 'ID', width: 50 },
            { dataField: 'name', caption: 'Product Name' },
            { dataField: 'price', caption: 'Price', width: 125, format: { type: 'currency', currency: 'INR' } },
            { dataField: 'manufacturer', caption: 'Manufacturer' },
            { dataField: 'inStock', caption: 'In Stock' }
        ],
        editing: {
            mode: 'popup', // cell, batch, popup, row, form
            allowAdding: true,
            allowUpdating: true,
            allowDeleting: true
        },
        searchPanel: {
            visible: true,
            highlightCaseSensitive: true
        },
        groupPanel: {
            visible: true
        },
        grouping: {
            autoExpandAll: true
        },
        filterRow: {
            visible: true
        },
        headerFilter: {
            visible: true
        },
        columnChooser: {
            enabled: true
        },
        columnFixing: {
            enabled: true
        },
        paging: {
            pageSize: 5
        },
        pager: {
            showPageSizeSelector: true,
            allowedPageSizes: [5, 10, 20],
            showInfo: true
        },
        remoteOperations: false,
        rowAlternationEnabled: true,
        showBorders: false,
        showColumnLines: false,
        // showRowLines: true,
        wordWrapEnabled: true,
        onCellPrepared: function (e) {
            if (e.rowType === "data" && e.column.dataField === "price") {
                if (e.data.price > 500) {
                    e.cellElement.css("color", "red");
                }
            }
        },
        onRowInserted: function (e) {
            console.log('Row inserted', e);
        },
        onRowUpdated: function (e) {
            console.log('Row updated', e);
        },
        onRowRemoved: function (e) {
            console.log('Row removed', e);
        }
    }).dxDataGrid('instance');
});