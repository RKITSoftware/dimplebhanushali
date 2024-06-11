$(() => {
    // Initialize checkboxes with specific configurations
    const checked = $('#checked').dxCheckBox({
        value: true,
        name: "Skill"
    }).dxCheckBox("instance");

    const unChecked = $('#unChecked').dxCheckBox({
        value: false,
        activeStateEnabled: true,
    }).dxCheckBox("instance");

    const disable = $('#disable').dxCheckBox({
        disabled: true,
    }).dxCheckBox("instance");

    const access = $('#access').dxCheckBox({
        accessKey: "c",
        hint: "Press Alt + C to select this checkbox.",
        focusStateEnabled: true,
    }).dxCheckBox("instance");

    const elementAttr = $('#elementAttr').dxCheckBox({
        elementAttr: {
            class: "element",
        },
    }).dxCheckBox("instance");

    const height = $('#height').dxCheckBox({
        // height: 50,
        // width: 50,
        tabIndex: 40,
        hoverStateEnabled: false,
        isValid: false,
    }).dxCheckBox("instance");

    const chkBoxWidget = $('#visible').dxCheckBox({
        value: true,
        visible: true,
        // readOnly: true,
        onContentReady: function (e) {
            console.log("Visible checkbox is ready");
        },
    }).dxCheckBox("instance");

    // Accessing the dxCheckBox instance
    const instance = $("#checked").dxCheckBox('instance');

    // Setting additional event handlers
    instance.option('onInitialized', function (e) {
        console.log("Visible checkbox has been initialized");
    });

    instance.option('onOptionChanged', function (e) {
        console.log(`Option ${e.name} has been changed to ${e.value}`);
    });

    instance.option('onValueChanged', function (e) {
        console.log(`Checkbox value has been changed to ${e.value}`);
    });

    const validationWidget = $('#validation').dxCheckBox({
        value: true,
        text: "Validation"
    }).dxCheckBox('instance');

    validationWidget.option({
        validationStatus: "invalid", // pending, valid
        // validationError: { message: "Invalid Operation" },
        validationMessageMode: 'auto', // always
        validationErrors: [
            { message: "Invalid Operation" },
            { message: "Another validation error" }
        ]
    });

    // Button event handlers
    $('#toggleChecked').click(() => {
        const currentValue = checked.option('value');
        checked.option('value', !currentValue);
    });

    $('#toggleUnchecked').click(() => {
        const currentValue = unChecked.option('value');
        unChecked.option('value', !currentValue);
    });

    $('#enableDisable').click(() => {
        const isDisabled = disable.option('disabled');
        disable.option('disabled', !isDisabled);
    });

    $('#toggleAccess').click(() => {
        const currentValue = access.option('value');
        access.option('value', !currentValue);
    });

    $('#toggleElementAttr').click(() => {
        const currentValue = elementAttr.option('value');
        elementAttr.option('value', !currentValue);
    });

    $('#toggleHeightWidth').click(() => {
        const currentHeight = height.option('height');
        const currentWidth = height.option('width');
        height.option({
            height: currentHeight === 50 ? 100 : 50,
            width: currentWidth === 50 ? 100 : 50
        });
    });

    $('#toggleReadOnly').click(() => {
        const isReadOnly = chkBoxWidget.option('readOnly');
        chkBoxWidget.option('readOnly', !isReadOnly);
    });

    $('#validate').click(() => {
        const currentStatus = validationWidget.option('validationStatus');
        validationWidget.option('validationStatus', currentStatus === "valid" ? "invalid" : "valid");
    });

    $('#labeled').dxCheckBox({
        value: true,
        text: 'Label',
    });

    // Button event handlers for method calls on the 'checked' checkbox instance
    $('#btnBeginUpdate').click(() => {
        checked.beginUpdate();
        console.log("beginUpdate() called on 'checked'");
    });

    $('#btnDefaultOptions').click(() => {
        DevExpress.ui.dxCheckBox.defaultOptions({
            options: {
                onValueChanged: function (e) {
                    console.log("Value changed globally", e.value);
                }
            }
        });
        console.log("defaultOptions(rule) called");
    });

    $('#btnDispose').click(() => {
        checked.dispose();
        console.log("dispose() called on 'checked'");
    });

    $('#btnElement').click(() => {
        const element = checked.element();
        console.log("element() called on 'checked'", element);
    });

    $('#btnEndUpdate').click(() => {
        checked.endUpdate();
        console.log("endUpdate() called on 'checked'");
    });

    $('#btnFocus').click(() => {
        checked.focus();
        console.log("focus() called on 'checked'");
    });

    $('#btnGetInstance').click(() => {
        const instance = DevExpress.ui.dxCheckBox.getInstance($("#checked"));
        console.log("getInstance(element) called on 'checked'", instance);
    });

    $('#btnInstance').click(() => {
        const instance = checked.instance();
        console.log("instance() called on 'checked'", instance);
    });

    $('#btnOffEventName').click(() => {
        checked.off("valueChanged");
        console.log("off(eventName) called on 'checked'");
    });

    $('#btnOffEventNameHandler').click(() => {
        const handler = function () { console.log("Event handler") };
        checked.on("valueChanged", handler);
        checked.off("valueChanged", handler);
        console.log("off(eventName, eventHandler) called on 'checked'");
    });

    $('#btnOnEventNameHandler').click(() => {
        checked.on("valueChanged", function (e) { console.log("Event handler", e.value) });
        console.log("on(eventName, eventHandler) called on 'checked'");
    });

    $('#btnOnEvents').click(() => {
        checked.on({
            valueChanged: function (e) { console.log("Event handler for valueChanged", e.value) }
        });
        console.log("on(events) called on 'checked'");
    });

    $('#btnOption').click(() => {
        const option = checked.option();
        console.log("option() called on 'checked'", option);
    });

    $('#btnOptionName').click(() => {
        const value = checked.option("value");
        console.log("option(optionName) called on 'checked'", value);
    });

    $('#btnOptionNameValue').click(() => {
        checked.option("value", false);
        console.log("option(optionName, optionValue) called on 'checked'");
    });

    $('#btnOptionOptions').click(() => {
        checked.option({ value: true, disabled: false });
        console.log("option(options) called on 'checked'");
    });

    $('#btnRegisterKeyHandler').click(() => {
        checked.registerKeyHandler("space", function (e) { console.log("Space key pressed") });
        console.log("registerKeyHandler(key, handler) called on 'checked'");
    });

    $('#btnRepaint').click(() => {
        checked.repaint();
        console.log("repaint() called on 'checked'");
    });

    $('#btnReset').click(() => {
        checked.reset();
        console.log("reset() called on 'checked'");
    });

    $('#btnResetOptionName').click(() => {
        checked.resetOption("value");
        console.log("resetOption(optionName) called on 'checked'");
    });
});
