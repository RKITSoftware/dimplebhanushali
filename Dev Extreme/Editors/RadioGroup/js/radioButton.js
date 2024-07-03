$(() => {
    const transportationModes = [
        { id: 1, text: 'Car', icon: 'car' },
        { id: 2, text: 'Airplane', icon: 'airplane' },
        { id: 3, text: 'Train', icon: 'train' },
        { id: 4, text: 'Bicycle', icon: 'bicycle' },
        { id: 5, text: 'Walk', icon: 'walk' }
    ];

    // Initialize DevExpress RadioGroup
    $('#transportationContainer').dxRadioGroup({
        dataSource: transportationModes,
        valueExpr: 'id',
        displayExpr: 'text',
        layout: 'vertical',
        itemTemplate: function (itemData) {
            return `<div><i class="dx-icon dx-icon-${itemData.icon}"></i><span>${itemData.text}</span></div>`;
        },
        onValueChanged: function (e) {
            const selectedMode = transportationModes.find(item => item.id === e.value);
            $('#selectedMode').text(selectedMode ? selectedMode.text : 'None');

            // Show different dxToast notifications based on selected mode
            switch (selectedMode.id) {
                case 1:
                    DevExpress.ui.notify('You selected Car.', 'info', 1500);
                    break;
                case 2:
                    DevExpress.ui.notify('You selected Airplane.', 'warning', 1500);
                    break;
                case 3:
                    DevExpress.ui.notify('You selected Train.', 'error', 1500);
                    break;
                case 4:
                    DevExpress.ui.notify('You selected Bicycle.', 'success', 1500);
                    break;
                case 5:
                    DevExpress.ui.notify('You selected Walk.', 'info', 1500);
                    break;
                default:
                    DevExpress.ui.notify('Invalid selection.', 'error', 1500);
                    break;
            }
        },
        elementAttr: {
            class: 'transportation-selector'
        },
        focusStateEnabled: true,
        hoverStateEnabled: true,
        width: '100%',
        height: 'auto',
        readOnly: false,
        tabIndex: 0,
        visible: true
    });

    // Change dot colors programmatically (if necessary)
    const radioButtonElements = $('.dx-radiobutton');
    radioButtonElements.each(function (index) {
        const dotElement = $(this).find('.dx-radiobutton-icon-dot');
        switch (index) {
            case 0:
                dotElement.css('background-color', '#1f77b4');
                break;
            case 1:
                dotElement.css('background-color', '#ff7f0e');
                break;
            case 2:
                dotElement.css('background-color', '#2ca02c');
                break;
            case 3:
                dotElement.css('background-color', '#d62728');
                break;
            case 4:
                dotElement.css('background-color', '#9467bd');
                break;
            default:
                break;
        }
    });
});
