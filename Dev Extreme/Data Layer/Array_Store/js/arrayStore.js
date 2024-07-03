$(() => {

    const arrayStore = new DevExpress.data.ArrayStore({
        data: movies,
        key: 'ID',
        errorHandler: (error) => {
            alert('Error Occurred => ' + error);
        },
        onInserted: (values, key) => {
            DevExpress.ui.notify(`Custom item created: ${key} => ${JSON.stringify(values)}`, 'success', 1500);
        },
        onInserting: (values) => {
            console.log('Inserting Values => ' + JSON.stringify(values));
        },
        onLoaded: (result) => {
            console.log('Data Loaded => ', result);
        },
        onLoading: (loadOptions) => {
            console.log('Loading Data with options => ', loadOptions);
        },
        onModified: () => {
            console.log('Data Modified');
        },
        onModifying: () => {
            console.log('Modifying Data');
        },
        onPush: (changes) => {
            console.log('Data Pushed => ', changes);
        },
        onRemoved: (key) => {
            console.log('Data Removed with key => ', key);
        },
        onRemoving: (key) => {
            console.log('Removing Data with key => ', key);
        },
        onUpdated: (key, values) => {
            console.log('Data Updated => ', key, values);
        },
        onUpdating: (key, values) => {
            console.log('Updating Data => ', key, values);
        }
    });

    // Example usage of ArrayStore methods
    console.log('ByKey:', arrayStore.byKey(1));
    console.log('CreateQuery:', arrayStore.createQuery());
    console.log('Key:', arrayStore.key());
    console.log('KeyOf:', arrayStore.keyOf(movies[0]));
    console.log('Load:', arrayStore.load());
    arrayStore.push([{ type: 'update', key: 1, data: { Title: '3 Idiots Updated' } }]);
    console.log('TotalCount:', arrayStore.totalCount());
    arrayStore.update(1, { Title: '3 Idiots Again Updated' });

    // Event handlers
    const eventHandler = (e) => console.log('Event:', e);
    arrayStore.on('inserted', eventHandler);
    arrayStore.off('inserted', eventHandler);
    arrayStore.on({
        inserted: eventHandler,
        removed: eventHandler
    });

    // Setting up the dxSelectBox
    const arraStoreWidget = $('#arrayStore').dxSelectBox({
        acceptCustomValue: true,
        placeholder: 'Select Here...',
        showClearButton: true,
        dataSource: arrayStore,
        displayExpr: 'Title',
        valueExpr: 'ID',
        itemTemplate: function (data) {
            return `<div class="movie-item">
                        <div class="movie-title">${data.Title}</div>
                    </div>`;
        },
        onCustomItemCreating: function (data) {
            data.customItem = null;
        },
        buttons: [{
            name: "add",
            location: "after",
            options: {
                icon: "add",
                onClick: function (e) {
                    var value = arraStoreWidget.option("text");
                    var id = movies[movies.length - 1].ID;
                    arrayStore.insert({ ID: ++id, Title: value });
                    console.log('Added Item: ', value);
                }
            }
        }, 'dropDown'],
        onValueChanged: function (e) {
            arrayStore.byKey(e.value)
                .done(function (dataItem) {
                    console.log(dataItem);
                })
                .fail(function (error) {
                    console.log(error);
                });
        }
    }).dxSelectBox('instance');
});