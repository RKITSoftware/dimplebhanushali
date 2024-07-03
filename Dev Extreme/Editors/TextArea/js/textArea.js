$(() => {
    const textAreaWidget = $('#textArea').dxTextArea({
        placeholder: "Enter Text Here.........",
        width: 750,
        height: 90,
        accessKey: "t",
        autoResizeEnabled: false,
        maxHeight: 500,
        spellCheck: true,
        stylingMode: 'filled',
        minHeight: 90,

        onChange: function (e) {
            console.log("On Change => ", e);
        },
        onCopy: function (e) {
            console.log("On Copy => ", e);
        },
        onCut: function (e) {
            console.log("On Cut => ", e);
        },
        onPaste: function (e) {
            console.log("On Paste => ", e);
        },
        onDisposing: function (e) {
            console.log("On Disposing => ", e);
        },
    }).dxTextArea('instance');

    // Create NumberBox for setting TextArea height
    $('#heightBox').dxNumberBox({
        min: 50,
        max: 500,
        value: 90,
        showSpinButtons: true,
        onValueChanged: function (e) {
            textAreaWidget.option('height', e.value);
            console.log("Height changed to:", e.value);
        }
    });

    // Repaint the widget after 3 seconds
    $('#repaintBtn').on('click', () => {
        setTimeout(() => {
            textAreaWidget.repaint();
            console.log("Repaint called after 3 seconds");
        }, 3000);
    });

    $('#disposeBtn').on('click', () => {
        textAreaWidget.dispose();
    });

    // Register a key handler
    textAreaWidget.registerKeyHandler("enter", function (e) {
        console.log("Enter key pressed", e);
    });

    // Add event listener
    textAreaWidget.on("focusIn", function (e) {
        console.log("Focus In", e);
    });

    // Remove an event listener
    textAreaWidget.off("focusIn");

    // Add multiple event listeners
    textAreaWidget.on({
        focusIn: function (e) {
            console.log("Focus In (multiple)", e);
        },
        focusOut: function (e) {
            console.log("Focus Out (multiple)", e);
        }
    });

    // Update option
    textAreaWidget.option("height", 120);
    console.log("Updated height:", textAreaWidget.option("height"));

    // Reset an option to its default value
    textAreaWidget.resetOption("height");
    console.log("Reset height:", textAreaWidget.option("height"));

    // Reset the widget
    $('#resetBtn').on('click', () => {
        textAreaWidget.reset();
    });

    // Other custom buttons for demonstration
    $('#blurBtn').on('click', () => {
        textAreaWidget.blur();
    });

    // Option method to get all current options
    console.log("Current options:", textAreaWidget.option());
});
