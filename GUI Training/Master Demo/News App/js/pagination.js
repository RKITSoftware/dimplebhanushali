// Function to generate pagination controls and update the current page button
function generatePagination(pageCount, currentPage) {
    const paginationContainer = document.getElementById("pagination-container");
    const prevButton = document.getElementById("prev_btn");
    const nextButton = document.getElementById("next_btn");
    const firstButton = document.getElementById("first_btn");
    const lastButton = document.getElementById("last_btn");
    const currentPageBtn = document.getElementById("current-page-btn");

    if (currentPage === 1) {
        prevButton.disabled = true;
        firstButton.disabled = true;
    } else {
        prevButton.disabled = false;
        firstButton.disabled = false;
    }

    if (currentPage === pageCount) {
        nextButton.disabled = true;
        lastButton.disabled = true;
    } else {
        nextButton.disabled = false;
        lastButton.disabled = false;
    }

    currentPageBtn.innerText = `Page ${currentPage}`;
}

// Function to navigate to a specific page
function goToPage(change) {
    currentPage += change;
    if (currentPage < 1) currentPage = 1; // Prevent going to a page less than 1
    fetchNewsAndBindData(currentQuery, currentPage);
}

// Function to navigate to the first page
function goToFirstPage() {
    currentPage = 1;
    fetchNewsAndBindData(currentQuery, currentPage);
}

// Function to navigate to the last page
function goToLastPage(pageCount) {
    currentPage = pageCount;
    fetchNewsAndBindData(currentQuery, currentPage);
}
