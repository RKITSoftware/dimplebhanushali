$(() => {

    const btnNormalWidget = $('#btnNormal').dxButton({
        text: 'Gift',
        stylingMode: 'contained', // 'contained, outlined, text'
        accessKey: 'b',
        type: 'success',
        icon: 'gift', //DevExtreme Library, url , 
        // disabled: true,
        onClick() {
            DevExpress.ui.notify('The Normal button was clicked');
        },
    }).dxButton('instance');

    const btnDangerWidget = $('#btnDanger').dxButton({
        text: 'Shinchan',
        stylingMode: 'text', // 'contained, outlined, text'
        accessKey: 'a',
        type: 'danger',
        icon: 'https://i.etsystatic.com/34580755/r/il/09d9f3/3712089394/il_fullxfull.3712089394_blxz.jpg',
        onClick() {
            DevExpress.ui.notify('The Danger button was clicked');
        },
    }).dxButton('instance');

    const btnBackWidget = $('#btnBack').dxButton({
        text: 'Default',
        stylingMode: 'outlined', // 'contained, outlined, text'
        accessKey: 'c',
        type: 'back',
        // icon: 'home',
        // useSubmitBehavior: true,
        // validationGroup: 'group1',
        onClick() {
            DevExpress.ui.notify('The Back button was clicked');
        },
    }).dxButton('instance');

    const btnSubmitWidget = $('#btnSubmit').dxButton({
        text: 'Submit',
        stylingMode: 'outlined', // 'contained, outlined, text'
        accessKey: 's',
        type: 'success',
        // icon: 'home',
        useSubmitBehavior: true,
        validationGroup: 'group1',
        onClick() {
            DevExpress.ui.notify('The Submit button was clicked');
        },
    }).dxButton('instance');

});