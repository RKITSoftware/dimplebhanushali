$(() => {
    const numberWidget = $('#numberBox').dxNumberBox({
        name: "age",
        placeholder: 'Enter a number',
        // onContentReady: function (e) {
        //     DevExpress.ui.notify('Number box content is ready!');
        // },
        // onCopy: function (e) {
        //     DevExpress.ui.notify('Content copied!');
        // },
        // onCut: function (e) {
        //     DevExpress.ui.notify('Content cut!');
        // },
        // onDisposing: function (e) {
        //     DevExpress.ui.notify('Number box is being disposed!');
        // },
        // onEnterKey: function (e) {
        //     DevExpress.ui.notify('Enter key pressed!');
        // },
        // onFocusIn: function (e) {
        //     DevExpress.ui.notify('Number box focused!');
        // },
        // onFocusOut: function (e) {
        //     DevExpress.ui.notify('Number box focus lost!');
        // },
        // onInitialized: function (e) {
        //     DevExpress.ui.notify('Number box initialized!');
        // },
        // onInput: function (e) {
        //     DevExpress.ui.notify('Input event triggered!');
        // },
        // onKeyDown: function (e) {
        //     DevExpress.ui.notify('Key down event triggered!');
        // },
        // onKeyUp: function (e) {
        //     DevExpress.ui.notify('Key up event triggered!');
        // },
        // onOptionChanged: function (e) {
        //     DevExpress.ui.notify('Option changed!');
        // },
        // onPaste: function (e) {
        //     DevExpress.ui.notify('Content pasted!');
        // },
        // onValueChanged: function (e) {
        //     DevExpress.ui.notify('Value changed to ' + e.value + '!');
        // }
    }).dxNumberBox('instance');

    // Additional methods usage
    numberWidget.focus();

    // Button click event handlers
    $('#defaultModeBtn').click(function () {
        numberWidget.option({
            buttons: ['spins'],
            showClearButton: false,
            readOnly: false,
            disabled: false,
            max: null,
            min: null,
            placeholder: 'Enter a number'
        });
    });

    // Spin Button
    $('#spinClearBtn').click(function () {
        numberWidget.option({
            buttons: ['spins', 'clear'],
            showClearButton: true,
            showSpinButtons: true,
            readOnly: false,
            disabled: false,
            max: null,
            min: null,
            placeholder: 'Enter a number'
        });
    });

    // Disable
    $('#disabledBtn').click(function () {
        numberWidget.option({
            disabled: true,
            readOnly: false,
            placeholder: ''
        });
    });

    // Max and Min
    $('#maxMinBtn').click(function () {
        numberWidget.option({
            disabled: false,
            readOnly: false,
            max: 100,
            min: 0,
            step: 5, // Check if step is being set correctly
            stylingMode: "underlined",
            placeholder: 'Enter a number between 0 and 100'
        });
    });

    // Event handling
    $('#eventHandlingBtn').click(function () {
        DevExpress.ui.notify('Event Handling button clicked');
        // You can add any custom logic here for event handling
    });

    // Sales
    $('#monthSalesBtn').click(function () {
        numberWidget.option({
            placeholder: 'Enter this month sales',
            readOnly: false,
            disabled: false,
            max: null,
            min: null,
            value: null
        });
    });

    // Stock Btn
    $('#stockBtn').click(function () {
        numberWidget.option({
            placeholder: 'Enter stock quantity',
            readOnly: false,
            disabled: false,
            max: null,
            min: null,
            value: null
        });
    });
});
