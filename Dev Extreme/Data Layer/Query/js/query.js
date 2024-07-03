$(() => {
    const initialData = [
        { id: 1, name: 'MacBook Pro', price: 12999 },
        { id: 2, name: 'Wireless Mouse', price: 490 },
        { id: 3, name: 'Mechanical Keyboard', price: 120 },
        { id: 4, name: 'Laptop Charger', price: 750 },
        { id: 5, name: 'USB-C Cable', price: 99 },
        { id: 6, name: 'External Hard Drive', price: 899 },
        { id: 7, name: 'Noise Cancelling Headphones', price: 999 },
        { id: 8, name: 'Smartphone', price: 9999 },
        { id: 9, name: 'Tablet', price: 4999 },
        { id: 10, name: 'Bluetooth Speaker', price: 1500 }
    ];

    let localData = initialData.slice();

    const localStore = new DevExpress.data.LocalStore({
        key: 'id',
        data: localData,
        name: 'products',
        flushInterval: 10000, // Save changes every 10 seconds
        immediate: true, // Save changes immediately after they are made
        onLoaded: function () {
            updateAggregates(); // Initial update after data is loaded
        },
        onInserted: function (values) {
            localData.push(values); // Add inserted data to localData
            updateAggregates(); // Update aggregates after insert
        },
        onUpdated: function (key, values) {
            const index = localData.findIndex(item => item.id === key);
            if (index !== -1) {
                Object.assign(localData[index], values); // Update localData
                updateAggregates(); // Update aggregates after update
            }
        },
        onRemoved: function (key) {
            localData = localData.filter(item => item.id !== key); // Remove item from localData
            updateAggregates(); // Update aggregates after remove
        }
    });

    function renderProducts(products) {
        const $productList = $('#productList tbody');
        $productList.empty();
        products.forEach(function (product) {
            const row = `<tr data-id="${product.id}">
                            <td>${product.name}</td>
                            <td>${product.price}</td>
                            <td>
                                <button class="edit" data-id="${product.id}">Edit</button>
                                <button class="delete" data-id="${product.id}">Delete</button>
                            </td>
                        </tr>`;
            $productList.append(row);
        });
    }

    function loadProducts() {
        renderProducts(localData);
        updateAggregates();
    }

    function updateAggregates() {
        dataSource.count().done(function (count) {
            $('#totalCount').text(count);
        });

        dataSource.avg('price').done(function (avgPrice) {
            $('#avgPrice').text(avgPrice.toFixed(2));
        });

        dataSource.min('price').done(function (minPrice) {
            $('#minPrice').text(minPrice);
        });

        dataSource.max('price').done(function (maxPrice) {
            $('#maxPrice').text(maxPrice);
        });

        dataSource.sum('price').done(function (sumPrice) {
            $('#sumPrice').text(sumPrice);
        });
    }

    $('#addProduct').off('click').on('click', function () {
        const newProductName = $('#productName').val().trim();
        const newProductPrice = parseFloat($('#productPrice').val());
        if (newProductName === '' || isNaN(newProductPrice)) return;

        const newProduct = {
            id: localData.length + 1,
            name: newProductName,
            price: newProductPrice
        };

        localStore.insert(newProduct).done(function () {
            $('#productName').val('');
            $('#productPrice').val('');
            localData.push(newProduct);
            loadProducts();
            DevExpress.ui.notify('Product added successfully', 'success', 2000);
        });
    });

    $('#productList').off('click', '.edit').on('click', '.edit', function () {
        const productId = parseInt($(this).data('id'));
        const product = localData.find(p => p.id === productId);
        if (!product) return;

        const newTitle = prompt('Enter new name for product:', product.name);
        const newPrice = parseFloat(prompt('Enter new price for product:', product.price));

        if (newTitle !== null && !isNaN(newPrice)) {
            localStore.update(productId, { name: newTitle, price: newPrice }).done(function () {
                const index = localData.findIndex(p => p.id === productId);
                if (index !== -1) {
                    localData[index].name = newTitle;
                    localData[index].price = newPrice;
                    loadProducts();
                    DevExpress.ui.notify(`Product with ID ${productId} updated successfully`, 'success', 2000);
                }
            });
        }
    });

    $('#productList').off('click', '.delete').on('click', '.delete', function () {
        const productId = parseInt($(this).data('id'));
        if (confirm(`Are you sure you want to delete product with ID ${productId}?`)) {
            localStore.remove(productId).done(function () {
                localData = localData.filter(p => p.id !== productId);
                loadProducts();
                DevExpress.ui.notify(`Product with ID ${productId} deleted successfully`, 'success', 2000);
            });
        }
    });

    const dataSource = DevExpress.data.query(initialData);

    function performAggregate() {
        const seed = 0;
        const step = (accumulator, value) => accumulator + value.price;
        const finalize = result => result / localData.length;

        dataSource.aggregate(seed, step, finalize).done(function (result) {
            $('#resultContainer').html(`<pre>${JSON.stringify(result)}</pre>`);
        });
    }

    $('#btnAggregate').off('click').on('click', performAggregate);

    function performFilter() {
        const filteredData = localData.filter(item => item.price > 30);
        renderProducts(filteredData);
        updateAggregates();
    }

    function performGroupBy() {
        const groupedData = localData.reduce((acc, curr) => {
            const key = curr.price;
            if (!acc[key]) {
                acc[key] = [];
            }
            acc[key].push(curr);
            return acc;
        }, {});
        const groupedArray = Object.values(groupedData).flatMap(group => group);
        renderProducts(groupedArray);
        updateAggregates();
    }

    function performSelect() {
        const selectedData = localData.map(item => ({ id: item.id, name: item.name }));
        renderProducts(selectedData);
        updateAggregates();
    }

    function performSortBy() {
        const sortedData = localData.slice().sort((a, b) => b.price - a.price);
        renderProducts(sortedData);
        updateAggregates();
    }

    function performThenBy() {
        const sortedData = localData.slice().sort((a, b) => a.price - b.price || a.name.localeCompare(b.name));
        renderProducts(sortedData);
        updateAggregates();
    }

    function performSlice() {
        const slicedData = localData.slice(2, 5);
        renderProducts(slicedData);
        updateAggregates();
    }

    function performToArray() {
        console.log(localData);
    }

    $('#btnFilter').off('click').on('click', performFilter);
    $('#btnGroupBy').off('click').on('click', performGroupBy);
    $('#btnSelect').off('click').on('click', performSelect);
    $('#btnSlice').off('click').on('click', performSlice);
    $('#btnSortBy').off('click').on('click', performSortBy);
    $('#btnThenBy').off('click').on('click', performThenBy);
    $('#btnToArray').off('click').on('click', performToArray);

    // Initial load
    loadProducts();
});
