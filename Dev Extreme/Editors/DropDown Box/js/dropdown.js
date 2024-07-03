$(() => {
    const data = [
        { id: 1, name: "Dimple", age: 23 },
        { id: 2, name: "Ishika", age: 21 },
        { id: 3, name: "Aarti", age: 210 },
        { id: 4, name: "Yash", age: 975 },
    ];

    const dropDownWidget = $('#dropDown').dxDropDownBox({
        valueExpr: 'id',
        accessKey: "x",
        displayExpr: 'name',
        placeholder: "Select Name Here.....",
        isValid: true,
        name: "name",
        validationMessageMode: 'always', // 'auto', 'always'
        validationStatus: 'valid', // Initially set to valid, 'valid', 'invalid', 'pending'
        validationErrors: [{ message: "Please select a valid item." }],
        showClearButton: true,
        hint: "Press Alt + X to Focus here !!!",
        hoverStateEnabled: true,
        acceptCustomValue: true, // Accept custom values (default is false)
        // visible: false, 
        // disabled: true, 
        // readOnly: true, 
        // rtlEnabled: true, 
        // height: 100,
        // width: 200,
        activeStateEnabled: true,
        dataSource: data,
        contentTemplate: (e) => {
            const $list = $("<div>").dxList({
                dataSource: e.component.getDataSource(),
                itemTemplate: function (item) {
                    // Create a <div> element to hold the item
                    var $itemDiv = $("<div>");

                    // Create a <span> element for the name and apply a CSS class
                    var $nameSpan = $("<span>").text(item.name).addClass("item-name");

                    // Create a <span> element for the age and apply a CSS class
                    var $ageSpan = $("<span>").text(" Age: " + item.age).addClass("item-age");

                    // Append the name and age spans to the item div
                    $itemDiv.append($nameSpan, $ageSpan);

                    // Return the constructed item div
                    return $itemDiv;
                },
                onItemClick: (selected) => {
                    e.component.option("value", selected.itemData.id);
                    validateDropDownBox(e.component);
                    console.log("value : ", e.component.option("value"));
                    console.log("selected : ", selected);
                    e.component.close();
                }
            });
            return $list;
        },
        onFocusIn: function (e) {
            console.log("Focus In");
        },
        onFocusOut: function (e) {
            console.log("Focus Out");
        },
        onInitialized: function (e) {
            console.log("Initialized");
        },
        onValueChanged: function (e) {
            console.log("Value Changed");
        },
        onOptionChanged: function (e) {
            console.log("Option Changed");
        },
        stylingMode: "underlined" // outlined, filled
    }).dxDropDownBox('instance');

    function validateDropDownBox(component) {
        const value = component.option("value");
        if (value === null || value === undefined) {
            component.option("validationStatus", "invalid");
            component.option("validationErrors", [{ message: "Please select a valid item." }]);
        } else {
            component.option("validationStatus", "valid");
            component.option("validationErrors", []);
        }
    }

    const makeAsyncDataSource = function (jsonFile) {
        return new DevExpress.data.CustomStore({
            loadMode: 'raw',
            key: 'id',
            load() {
                return $.getJSON(jsonFile);
            },
        });
    };

    const jsonWidget = $("#jsonDropDown").dxDropDownBox({
        placeholder: "Select From DropDown.........",
        displayExpr: function (item) {
            return item ? `${item.name} (Email: ${item.email})` : "";
        },
        valueExpr: "id",
        dataSource: makeAsyncDataSource('Data/Data.json'),
        contentTemplate: (e) => {
            const $list = $("<div>").dxList({
                dataSource: e.component.getDataSource(),
                selectionMode: 'multiple',
                itemTemplate: function (item) {
                    const $item = $("<div>").addClass("item");

                    // Display each property of the item
                    $item.append($("<div>").text(`Name: ${item.name}`));
                    $item.append($("<div>").text(`Age: ${item.age}`));
                    $item.append($("<div>").text(`Occupation: ${item.occupation}`));
                    $item.append($("<div>").text(`Email: ${item.email}`));

                    // Display skills as a list
                    const $skills = $("<div>").text(`Skills: `);
                    const $skillsList = $("<ul>");
                    item.skills.forEach(skill => {
                        $skillsList.append($("<li>").text(skill));
                    });
                    $skills.append($skillsList);
                    $item.append($skills);

                    return $item;
                },
                onItemClick: (selected) => {
                    e.component.option("value", selected.itemData.id);
                    console.log("value : ", e.component.option("value"));
                    console.log("selected : ", selected);
                    e.component.close();
                }
            });
            return $list;
        },
        dropDownOptions: {
            width: 600,
            height: 500,
            closeOnOutsideClick: true,
        },
        stylingMode: "filled", // outlined, filled
        opened: false
    }).dxDropDownBox("instance");

    const eventsWidget = $("#eventBox").dxDropDownBox({
        dataSource: data,
        hint: "drop Down",
        displayExpr: "name",
        valueExpr: "id",
        placeholder: "Select Name",
        acceptCustomValue: true,
        showClearButton: true,
        contentTemplate: (e) => {
            const $list = $("<div>").dxList({
                dataSource: e.component.getDataSource(),
                itemTemplate: function (item) {
                    return $("<div>").text(item.name);
                },
                onItemClick: (selected) => {
                    e.component.option("value", selected.itemData.id);
                    //  console.log("value : ", e.component.option("value"));
                    //  console.log("selected : ", selected);
                    e.component.close();
                }
            });
            return $list;
        },
        stylingMode: "underlined"
    }).dxDropDownBox("instance");

    // Using the methods
    eventsWidget.blur(); // Blurs the dropdown
    eventsWidget.close(); // Closes the dropdown
    const content = eventsWidget.content(); // Gets the content of the dropdown
    console.log("content => ", content);
    eventsWidget.focus(); // Focuses on the dropdown
    const dataSource = eventsWidget.getDataSource(); // Gets the data source instance associated with the dropdown

    // Other methods - Remove or modify the method calls as needed
    // eventsWidget.beginUpdate(); // Not commonly used with dropdowns
    // eventsWidget.defaultOptions(rule); // Remove this line or define 'rule' and the default options object if needed
    // eventsWidget.dispose(); // Not necessary unless you explicitly want to dispose of the dropdown
    // eventsWidget.element(); // This retrieves the root element, but it's not commonly used directly
    // eventsWidget.endUpdate(); // Not commonly used with dropdowns
    // eventsWidget.field(); // Not commonly used with dropdowns
    // eventsWidget.getButton(name); // Not commonly used with dropdowns
    // eventsWidget.getInstance(element); // Not commonly used with dropdowns
    // eventsWidget.instance(); // Not commonly used with dropdowns
    // eventsWidget.off(eventName); // Not commonly used with dropdowns
    // eventsWidget.off(eventName, eventHandler); // Not commonly used with dropdowns
    // eventsWidget.on(eventName, eventHandler); // Not commonly used with dropdowns
    // eventsWidget.on(events); // Not commonly used with dropdowns
    // eventsWidget.open(); // You've already called 'open()' while initializing the dropdown
    // eventsWidget.option(); // You can use this to get or set options dynamically if needed
    // eventsWidget.option(optionName); // Gets the value of a specific option of the dropdown
    // eventsWidget.option(optionName, optionValue); // Sets a specific option of the dropdown
    // eventsWidget.option(options); // Sets several options of the dropdown at once
    // eventsWidget.registerKeyHandler(key, handler); // Registers a handler for a specific keyboard key
    // eventsWidget.repaint(); // Not commonly used with dropdowns
    // eventsWidget.reset(); // Resets the dropdown
    // eventsWidget.resetOption(optionName); // Resets the value of the specified option
});


// itemTemplate: function (item) {
//     // Create a <table> element to hold the item details
//     var $table = $("<table>").addClass("item-table");

//     // Create a <tr> element for each item property (name, age, etc.)
//     var $nameRow = $("<tr>").addClass("item-row");
//     var $nameCell = $("<td>").addClass("item-cell").text("Name:");
//     var $nameValueCell = $("<td>").addClass("item-cell").text(item.name);
//     $nameRow.append($nameCell, $nameValueCell);

//     var $ageRow = $("<tr>").addClass("item-row");
//     var $ageCell = $("<td>").addClass("item-cell").text("Age:");
//     var $ageValueCell = $("<td>").addClass("item-cell").text(item.age);
//     $ageRow.append($ageCell, $ageValueCell);

//     // Append the rows to the table
//     $table.append($nameRow, $ageRow);

//     // Wrap the table in a <div> for proper rendering
//     var $containerDiv = $("<div>").append($table);

//     // Return the constructed container div
//     return $containerDiv;
// },