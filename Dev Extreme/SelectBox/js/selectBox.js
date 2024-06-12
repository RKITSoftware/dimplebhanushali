$(() => {

    const genres = [
        { key: 'Comedy-Drama', items: movies.filter(movie => movie.Genre === 'Comedy-Drama') },
        { key: 'Biography', items: movies.filter(movie => movie.Genre === 'Biography') },
        { key: 'Sport-Drama', items: movies.filter(movie => movie.Genre === 'Sport-Drama') },
        { key: 'Musical-Drama', items: movies.filter(movie => movie.Genre === 'Musical-Drama') },
        { key: 'Romantic-Drama', items: movies.filter(movie => movie.Genre === 'Romantic-Drama') },
        { key: 'Action-Fantasy', items: movies.filter(movie => movie.Genre === 'Action-Fantasy') },
        { key: 'Action-Comedy', items: movies.filter(movie => movie.Genre === 'Action-Comedy') },
        { key: 'Drama', items: movies.filter(movie => movie.Genre === 'Drama') },
    ];

    const selectBoxWidget = $('#selectBox').dxSelectBox({
        acceptCustomValue: true,
        items: simpleMovies,
        placeholder: 'Select Here...',
        showDropDownButton: true,
        useItemTextAsTitle: true,
        showClearButton: true,
        value: simpleMovies[1],
    }).dxSelectBox('instance');

    const newWidget = $('#dataBox').dxSelectBox({
        acceptCustomValue: true,
        placeholder: 'Select Here...',
        showClearButton: true,
        noDataText: 'Kuch Nahi he Dikhane ki !!!!',
        dataSource: new DevExpress.data.ArrayStore({
            data: movies,
            key: 'key',
        }),
        displayExpr: 'Title',
        valueExpr: 'ID',
        onValueChanged: function (e) {
            const selectedMovie = movies.find(movie => movie.ID === e.value);
            if (selectedMovie) {
                DevExpress.ui.notify(`Selected movie: ${selectedMovie.Title}\nDirector: ${selectedMovie.Director}\nRelease Year: ${selectedMovie.Release_Year}\nGenre: ${selectedMovie.Genre}\nRating: ${selectedMovie.Rating}`);
            }
        },
        itemTemplate: function (data) {
            return `<div class="movie-item">
                    <img src="${data.PosterSrc}" alt="${data.Title}" class="movie-poster">
                    <div class="movie-details">
                        <div class="movie-title">${data.Title}</div>
                        <div class="movie-info">Director: ${data.Director}</div>
                        <div class="movie-info">Year: ${data.Release_Year}</div>
                        <div class="movie-info">Genre: ${data.Genre}</div>
                        <div class="movie-info">Rating: ${data.Rating}</div>
                    </div>
                </div>`;
        },
        searchEnabled: true,
        showSelectionControls: true,
        searchExpr: ['Title', 'Director', 'Genre'],
        searchMode: 'contains',
        searchTimeout: 200,
        minSearchLength: 1,
        showDataBeforeSearch: true,
        maxLength: 100,
        dropDownButtonTemplate: function () {
            return '<i class="dx-icon dx-icon-search"></i>';
        },
        dropDownOptions: {
            width: 500,
            height: 400
        },
        useItemTextAsTitle: true,
        valueChangeEvent: 'change',
        wrapItemText: true,
        buttons: ['clear', {
            name: 'custom',
            location: 'after',
            options: {
                icon: 'check',
                hint: 'Confirm Selection',
                onClick: function () {
                    const selectedItem = newWidget.option('selectedItem');
                    if (selectedItem) {
                        DevExpress.ui.notify(`Confirmed selection: ${selectedItem.Title}`);
                    }
                }
            }
        }],
    }).dxSelectBox('instance');

    // Functionality for the third select box (Item Creating Select Box)
    const itemCreatingWidget = $('#itemCreatingBox').dxSelectBox({
        acceptCustomValue: true,
        placeholder: 'Select Here...',
        showClearButton: true,
        noDataText: 'Kuch Nahi he Dikhane ki !!!!',
        dataSource: new DevExpress.data.ArrayStore({
            data: movies,
            key: 'key',
        }),
        displayExpr: 'Title',
        valueExpr: 'ID',
        onValueChanged: function (e) {
            const selectedMovie = movies.find(movie => movie.ID === e.value);
            if (selectedMovie) {
                DevExpress.ui.notify(`Selected movie: ${selectedMovie.Title}\nDirector: ${selectedMovie.Director}\nRelease Year: ${selectedMovie.Release_Year}\nGenre: ${selectedMovie.Genre}\nRating: ${selectedMovie.Rating}`);
            }
        },
        itemTemplate: function (data) {
            return `<div class="movie-item">
                    <img src="${data.PosterSrc}" alt="${data.Title}" class="movie-poster">
                    <div class="movie-details">
                        <div class="movie-title">${data.Title}</div>
                        <div class="movie-info">Director: ${data.Director}</div>
                        <div class="movie-info">Year: ${data.Release_Year}</div>
                        <div class="movie-info">Genre: ${data.Genre}</div>
                        <div class="movie-info">Rating: ${data.Rating}</div>
                    </div>
                </div>`;
        },
        onCustomItemCreating: function (e) {
            const newId = movies.length + 1;
            const newMovie = {
                ID: newId,
                Title: e.text,
                Director: 'Unknown',
                Release_Year: new Date().getFullYear(),
                Genre: 'Custom',
                Rating: 'N/A',
                PosterSrc: 'images/movies/default.jpg', // Default image path
            };

            // Add the new movie to the data source
            movies.push(newMovie);
            genres.push({ key: 'Custom', items: [newMovie] });

            e.customItem = newMovie.ID; // Return the ID of the new item
            DevExpress.ui.notify(`Custom item created: ${e.text}`);
        },
        searchEnabled: true,
        searchExpr: ['Title', 'Director', 'Genre'],
        searchMode: 'contains',
        searchTimeout: 200,
        minSearchLength: 1,
        showDataBeforeSearch: true,
        maxLength: 100,
        dropDownButtonTemplate: function () {
            return '<i class="dx-icon dx-icon-search"></i>';
        },
        dropDownOptions: {
            width: 500,
            height: 400
        },
        useItemTextAsTitle: true,
        valueChangeEvent: 'change',
        wrapItemText: true,
        buttons: ['clear', {
            name: 'custom',
            location: 'after',
            options: {
                icon: 'check',
                hint: 'Confirm Selection',
                onClick: function () {
                    const selectedItem = newWidget.option('selectedItem');
                    if (selectedItem) {
                        DevExpress.ui.notify(`Confirmed selection: ${selectedItem.Title}`);
                    }
                }
            }
        }],
    }).dxSelectBox('instance');

    // Functionality for the fourth select box (Field Template Select Box)
    const fieldWidget = $('#fieldsBox').dxSelectBox({
        acceptCustomValue: true,
        placeholder: 'Select Here...',
        showClearButton: true,
        noDataText: 'Kuch Nahi he Dikhane ki !!!!',
        dataSource: new DevExpress.data.ArrayStore({
            data: movies,
            key: 'key',
        }),
        displayExpr: 'Title',
        valueExpr: 'ID',
        onValueChanged: function (e) {
            const selectedMovie = movies.find(movie => movie.ID === e.value);
            if (selectedMovie) {
                DevExpress.ui.notify(`Selected movie: ${selectedMovie.Title}\nDirector: ${selectedMovie.Director}\nRelease Year: ${selectedMovie.Release_Year}\nGenre: ${selectedMovie.Genre}\nRating: ${selectedMovie.Rating}`);
            }
        },
        itemTemplate: function (data) {
            return `<div class="movie-item">
                    <img src="${data.PosterSrc}" alt="${data.Title}" class="movie-poster">
                    <div class="movie-details">
                        <div class="movie-title">${data.Title}</div>
                        <div class="movie-info">Director: ${data.Director}</div>
                        <div class="movie-info">Year: ${data.Release_Year}</div>
                        <div class="movie-info">Genre: ${data.Genre}</div>
                        <div class="movie-info">Rating: ${data.Rating}</div>
                    </div>
                </div>`;
        },
        dropDownButtonTemplate: function () {
            return '<i class="dx-icon dx-icon-search"></i>';
        },
        fieldTemplate: function (selectedItem, fieldElement) {
            const fieldContainer = $('<div>').addClass('custom-field-template').appendTo(fieldElement);
            const inputElement = $('<div>').appendTo(fieldContainer);

            inputElement.dxTextBox({
                readOnly: true,
                value: selectedItem ? selectedItem.Title : '',
                placeholder: 'Select Here...'
            });

            if (selectedItem) {
                fieldContainer.append($('<img>').attr('src', selectedItem.PosterSrc).addClass('field-movie-poster'));

                const movieDetails = $('<div>').addClass('movie-details').appendTo(fieldContainer);
                $('<div>').addClass('movie-info').text('Genre: ' + (selectedItem.Genre || 'N/A')).appendTo(movieDetails);
                $('<div>').addClass('movie-info').text('Rating: ' + (selectedItem.Rating || 'N/A')).appendTo(movieDetails);
            }
        },
        useItemTextAsTitle: true,
        valueChangeEvent: 'change',
        wrapItemText: true,
        buttons: ['clear', {
            name: 'custom',
            location: 'after',
            options: {
                icon: 'check',
                hint: 'Confirm Selection',
                onClick: function () {
                    const selectedItem = newWidget.option('selectedItem');
                    if (selectedItem) {
                        DevExpress.ui.notify(`Confirmed selection: ${selectedItem.Title}`);
                    }
                }
            }
        }],
    }).dxSelectBox('instance');

    const grpWidget = $('#grpBox').dxSelectBox({
        acceptCustomValue: true,
        placeholder: 'Select Here...',
        showClearButton: true,
        noDataText: 'Kuch Nahi he Dikhane ki !!!!',
        dataSource: new DevExpress.data.ArrayStore({
            data: genres,
            key: 'key',
        }),
        displayExpr: 'Title',
        valueExpr: 'ID',
        onValueChanged: function (e) {
            const selectedMovie = movies.find(movie => movie.ID === e.value);
            if (selectedMovie) {
                DevExpress.ui.notify(`Selected movie: ${selectedMovie.Title}\nDirector: ${selectedMovie.Director}\nRelease Year: ${selectedMovie.Release_Year}\nGenre: ${selectedMovie.Genre}\nRating: ${selectedMovie.Rating}`);
            }
        },
        itemTemplate: function (data) {
            return `<div class="movie-item">
                    <img src="${data.PosterSrc}" alt="${data.Title}" class="movie-poster">
                    <div class="movie-details">
                        <div class="movie-title">${data.Title}</div>
                        <div class="movie-info">Director: ${data.Director}</div>
                        <div class="movie-info">Year: ${data.Release_Year}</div>
                        <div class="movie-info">Genre: ${data.Genre}</div>
                        <div class="movie-info">Rating: ${data.Rating}</div>
                    </div>
                </div>`;
        },
        dropDownButtonTemplate: function () {
            return '<i class="dx-icon dx-icon-search"></i>';
        },
        grouped: true,
        groupTemplate: function (groupData) {
            return `<div class="group-header">${groupData.key}</div>`;
        },
    }).dxSelectBox('instance');

});