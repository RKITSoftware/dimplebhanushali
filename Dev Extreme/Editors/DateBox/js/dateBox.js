$(() => {
    const now = new Date();
    const date = new Date(2018, 9, 16, 15, 8, 12);

    const federalHolidays = function (args) {
        const holidays = [
            new Date(2017, 0, 1),
            new Date(2017, 11, 25)
        ];
        return holidays.some(holiday => args.date.getTime() === holiday.getTime());
    };

    $('#default').dxDateBox({
        placeholder: '12/31/2018, 2:52 PM',
        type: 'datetime',
        showClearButton: true,
        useMaskBehavior: true,
        inputAttr: { 'aria-label': 'Date Time' },
    });

    $('#constant').dxDateBox({
        placeholder: '10/16/2018',
        showClearButton: true,
        useMaskBehavior: true,
        displayFormat: 'shortdate',
        type: 'date',
        value: date,
        inputAttr: { 'aria-label': 'Date' },
    });

    $('#pattern').dxDateBox({
        placeholder: 'Tuesday, 16 of Oct, 2018 14:52',
        showClearButton: true,
        useMaskBehavior: true,
        displayFormat: 'EEEE, d of MMM, yyyy HH:mm',
        value: date,
        inputAttr: { 'aria-label': 'Date' },
    });

    $('#escape').dxDateBox({
        placeholder: 'Year: 2018',
        showClearButton: true,
        useMaskBehavior: true,
        displayFormat: "'Year': yyyy",
        type: 'date',
        value: date,
        inputAttr: { 'aria-label': 'Date' },
    });

    $('#date').dxDateBox({
        type: 'date',
        value: now,
        inputAttr: { 'aria-label': 'Date' },
    });

    $('#time').dxDateBox({
        type: 'time',
        value: now,
        inputAttr: { 'aria-label': 'Time' },
    });

    $('#date-time').dxDateBox({
        type: 'datetime',
        value: now,
        inputAttr: { 'aria-label': 'Date Time' },
    });

    $('#custom').dxDateBox({
        displayFormat: 'EEEE, MMM dd',
        value: new Date(2001, 0, 11), // Custom date set to January 11, 2001
        inputAttr: { 'aria-label': 'Custom Date' },
    });

    $('#date-by-picker').dxDateBox({
        pickerType: 'rollers',
        value: now,
        inputAttr: { 'aria-label': 'Picker Date' },
    });

    $('#disabled').dxDateBox({
        type: 'datetime',
        disabled: true,
        value: now,
        inputAttr: { 'aria-label': 'Disabled' },
    });

    $('#disabledDates').dxDateBox({
        type: 'date',
        pickerType: 'calendar',
        value: new Date(2017, 0, 3),
        disabledDates: federalHolidays,
        inputAttr: { 'aria-label': 'Disabled' },
    });

    $('#clear').dxDateBox({
        type: 'time',
        showClearButton: true,
        value: new Date(2015, 11, 1, 6),
        inputAttr: { 'aria-label': 'Clear Date' },
    });

    $('#birthday').dxDateBox({
        applyValueMode: 'useButtons',
        value: new Date(2001, 0, 11), // Setting the start date to January 11, 2001
        max: new Date(),
        min: new Date(1900, 0, 1),
        inputAttr: { 'aria-label': 'Birth Date' },
        onValueChanged(data) {
            dateDiff(new Date(data.value));
        },
    });

    function dateDiff(date) {
        const diffInDay = Math.floor(Math.abs((new Date() - date) / (24 * 60 * 60 * 1000)));
        return $('#age').text(`${diffInDay} days`);
    }

    dateDiff(new Date(2001, 0, 11));

    // Normal Date Box
    $('#dateBoxNormal').dxDateBox({
        adaptivityEnabled: true,
        acceptCustomValue: true,
        type: 'date',
        pickerType: 'calendar',
        applyValueMode: 'useButtons',
        cancelButtonText: 'Cancel',
        applyButtonText: 'Apply',
    });

    // Date Box with Min and Max Dates
    $('#dateBoxMinMax').dxDateBox({
        type: 'date',
        min: new Date(2023, 0, 1),
        max: new Date(2023, 11, 31)
    });

    // Date Box with Custom Placeholder
    $('#dateBoxPlaceholder').dxDateBox({
        placeholder: 'Select a date...'
    });

    // Date Box with Disabled Dates
    $('#dateBoxDisabledDates').dxDateBox({
        type: 'date',
        disabledDates: function (args) {
            const dayOfWeek = args.date.getDay();
            return dayOfWeek === 0 || dayOfWeek === 6; // Disable weekends
        }
    });

    // Date Box with Validation
    $('#dateBoxValidation').dxDateBox({
        type: 'date',
        isValid: false,
        validationError: { message: "The date is not valid!" }
    });

    // // Date Box with Various Options
    // $('#dateBoxVarious').dxDateBox({
    //     adaptivityEnabled: true,
    //     acceptCustomValue: true,
    //     accessKey: 'd',
    //     activeStateEnabled: true,
    //     applyButtonText: 'Apply',
    //     applyValueMode: 'useButtons',
    //     buttons: ['clear', 'calendar'],
    //     calendarOptions: {
    //         firstDayOfWeek: 1
    //     },
    //     cancelButtonText: 'Cancel',
    //     dateOutOfRangeMessage: 'Date is out of range',
    //     dateSerializationFormat: 'yyyy-MM-ddTHH:mm:ssZ',
    //     deferRendering: false,
    //     disabled: false,
    //     dropDownButtonComponent: 'dxButton',
    //     dropDownButtonRender: null,
    //     dropDownButtonTemplate: 'customButton',
    //     dropDownOptions: { shading: true },
    //     elementAttr: { class: 'custom-attr' },
    //     focusStateEnabled: true,
    //     height: 'auto',
    //     hint: 'Choose a date',
    //     hoverStateEnabled: true,
    //     inputAttr: { id: 'dateBoxVariousInput' },
    //     interval: 30,
    //     invalidDateMessage: 'The date format is invalid',
    //     isValid: true,
    //     max: new Date(2025, 11, 31),
    //     maxLength: 10,
    //     min: new Date(2000, 0, 1),
    //     name: 'dateBoxVarious',
    //     onChange: function () { console.log('Changed'); },
    //     onClosed: function () { console.log('Closed'); },
    //     onContentReady: function () { console.log('Content Ready'); },
    //     onCopy: function () { console.log('Copied'); },
    //     onCut: function () { console.log('Cut'); },
    //     onDisposing: function () { console.log('Disposing'); },
    //     onFocusIn: function () { console.log('Focused In'); },
    //     onEnterKey: function () { console.log('Enter Key'); },
    //     onFocusOut: function () { console.log('Focused Out'); },
    //     onInitialized: function () { console.log('Initialized'); },
    //     onInput: function () { console.log('Input'); },
    //     onKeyDown: function () { console.log('Key Down'); },
    //     onKeyUp: function () { console.log('Key Up'); },
    //     onOpened: function () { console.log('Opened'); },
    //     onOptionChanged: function () { console.log('Option Changed'); },
    //     onPaste: function () { console.log('Pasted'); },
    //     onValueChanged: function () { console.log('Value Changed'); },
    //     opened: false,
    //     openOnFieldClick: true,
    //     pickerType: 'calendar',
    //     placeholder: 'Select a date',
    //     readOnly: false,
    //     rtlEnabled: false,
    //     showAnalogClock: false,
    //     showClearButton: true,
    //     showDropDownButton: true,
    //     spellcheck: true,
    //     stylingMode: 'underlined',
    //     tabIndex: 0,
    //     text: '',
    //     type: 'datetime',
    //     useMaskBehavior: true,
    //     validationError: { message: 'Date is not valid' },
    //     validationErrors: [{ message: 'Date is required' }],
    //     validationMessageMode: 'always',
    //     validationStatus: 'valid',
    //     value: new Date(),
    //     valueChangeEvent: 'change',
    //     visible: true,
    //     width: '100%'
    // });
});
