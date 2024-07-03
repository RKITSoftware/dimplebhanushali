document.addEventListener("DOMContentLoaded", function () {
    createSidebar();
});

function createSidebar() {
    const sidebar = document.createElement('aside'); // Change ul to aside
    sidebar.className = 'sidebar';

    const links = [
        { text: 'Array Store', href: '../Array_Store/index.html' },
        { text: 'Custom Store', href: '../Custom_Store/index.html' },
        { text: 'Data Source', href: '../Data_Source/index.html' },
        { text: 'Local Store', href: '../Local_Store/index.html' },
        { text: 'Query', href: '../Query/index.html' },
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
