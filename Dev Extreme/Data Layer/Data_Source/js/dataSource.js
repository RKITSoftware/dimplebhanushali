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

    const dataSourceConfig = {
        // customQueryParams: {
        //     category: 'electronics',
        //     storeLocation: 'online'
        // },
        // expand: ['manufacturerDetails', 'warrantyInfo'],
        store: initialData,
        // filter: ['price', '>', 50],
        map: function (item) {
            item.fullName = `${item.name} - ${item.manufacturer}`;
            return item; s
        },
        // postProcess: function (results) {
        //     return results.filter(item => item.inStock);
        // },
        searchExpr: ['name', 'manufacturer'],
        // searchExpr: 'price',
        searchOperation: 'contains',
        // searchValue: 'Apple',
        // select: ['id', 'name', 'price'],
        // sort: [{ field: 'price', desc: true }, { field: 'id', desc: false }],
        paginate: true,
        requireTotalCount: true,
        // pageSize: 10,

        group: 'manufacturer',
        onChanged: function (e) {
            console.log('Data changed:', e);
        },
        onLoadError: function (error) {
            console.error('Data loading error:', error);
        },
        onLoadingChanged: function (isLoading) {
            console.log('Loading state changed:', isLoading);
        },
        pushAggregationTimeout: 100,
        reshapeOnPush: true,

    };

    const dataSource = new DevExpress.data.DataSource(dataSourceConfig);

    console.log(dataSource.isLastPage());
    console.log(dataSource.items());
    console.log(dataSource.pageIndex());
    console.log(dataSource.pageSize());
    // Changing the search expression dynamically
    // dataSource.searchExpr('manufacturer');
    console.log(dataSource.searchExpr());
    console.log('total count => ', dataSource.requireTotalCount(true));

    $("#dataGrid").dxDataGrid({
        dataSource: dataSource,
        groupPanel: {
            visible: false
        },
        columns: [
            { dataField: "id", caption: "ID", width: 50 },
            { dataField: "fullName", caption: "Product Name" },
            { dataField: "manufacturer", caption: "Company" },
            { dataField: "inStock", caption: "Available" },
            { dataField: "price", caption: "Price", format: { type: "currency", currency: "INR" } }
        ]
    });
});
