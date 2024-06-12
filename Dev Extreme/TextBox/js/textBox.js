$(() => {
    // Initialize the TextBox with all specified options
    const textBox = $('#textBox').dxTextBox({
        mask: "+91 (X00) 000-0000",
        maskChar: "_",
        maskInvalidMessage: "The phone number must follow the pattern: +91 (X00) 000-0000",
        maskRules: {
            "X": /[02-9]/
        },
        placeholder: "Enter phone number...",
        showClearButton: true,
        valueChangeEvent: "keyup",
        accessKey: "t",
        activeStateEnabled: true,
        buttons: ['clear', {
            name: 'custom',
            location: 'after',
            options: {
                icon: 'check',
                onClick: () => { console.log("Custom button clicked"); }
            }
        }],
        disabled: false,
        elementAttr: { id: "myTextBox", class: "custom-class" },
        focusStateEnabled: true,
        height: 40,
        hint: "Enter your phone number",
        hoverStateEnabled: true,
        inputAttr: { maxlength: 50 },
        isValid: true,
        maxLength: 15,
        mode: "text",
        name: "phoneTextBox",
        onContentReady: function (e) {
            console.log("Content ready: ", e);
        },
        onValueChanged: function (e) {
            console.log("Value changed: ", e.value);
        },
        onFocusIn: function (e) {
            console.log("Focus in: ", e);
        },
        onFocusOut: function (e) {
            console.log("Focus out: ", e);
        },
        onInput: function (e) {
            console.log("Input: ", e);
        },
        onChange: function (e) {
            console.log("Change: ", e);
        },
        onCopy: function (e) {
            console.log("Copy: ", e);
        },
        onCut: function (e) {
            console.log("Cut: ", e);
        },
        onDisposing: function (e) {
            console.log("Disposing: ", e);
        },
        onEnterKey: function (e) {
            console.log("Enter key: ", e);
        },
        onInitialized: function (e) {
            console.log("Initialized: ", e);
        },
        onKeyDown: function (e) {
            console.log("Key down: ", e);
        },
        onKeyUp: function (e) {
            console.log("Key up: ", e);
        },
        onOptionChanged: function (e) {
            console.log("Option changed: ", e);
        },
        onPaste: function (e) {
            console.log("Paste: ", e);
        },
        readOnly: false,
        rtlEnabled: false,
        showMaskMode: "always",
        spellcheck: true,
        stylingMode: "underlined",
        tabIndex: 0,
        text: "",
        useMaskedValue: true,
        validationError: { message: "Invalid phone number" },
        validationErrors: [{ message: "Invalid phone number format" }],
        validationMessageMode: "auto",
        validationStatus: "valid",
        value: "",
        visible: true,
        width: 300
    }).dxTextBox('instance');
});
