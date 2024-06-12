document.addEventListener("DOMContentLoaded", function () {
    createSidebar();
});

function createSidebar() {
    const sidebar = document.createElement('aside'); // Change ul to aside
    sidebar.className = 'sidebar';

    const links = [
        { text: 'Check Box', href: '../CheckBox/index.html' },
        { text: 'Date Box', href: '../../DateBox/index.html' },
        { text: 'Drop Down Box', href: '../../DropDown Box/index.html' },
        { text: 'Number Box', href: '../Number Box/index.html' },
        { text: 'Select Box', href: '../SelectBox/index.html' },
        { text: 'Text Area', href: '../TextArea/index.html' },
        { text: 'Text Box', href: '../TextBox/index.html' },
        { text: 'Button', href: '../Button/index.html' },
        { text: 'File Uploader', href: '../FileUploader/index.html' },
        { text: 'Validation', href: '../Validation/index.html' },
        { text: 'Radio Group', href: '../RadioGroup/index.html' }
    ];

    links.forEach(linkData => {
        const listItem = document.createElement('li');
        const link = document.createElement('a');
        link.href = linkData.href;
        link.textContent = linkData.text;
        listItem.appendChild(link);
        sidebar.appendChild(listItem);
    });

    document.body.insertBefore(sidebar, document.body.firstChild);
}
