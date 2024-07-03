document.addEventListener("DOMContentLoaded", function () {
    createSidebar();
});

function createSidebar() {
    const sidebar = document.createElement('aside'); // Change ul to aside
    sidebar.className = 'sidebar';

    const links = [
        { text: 'Array Binding', href: '../DataBinding/Array/index.html' },
        { text: 'Ajax Binding', href: '../DataBinding/Ajax/index.html' },
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
