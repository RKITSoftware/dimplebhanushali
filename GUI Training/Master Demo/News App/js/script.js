// API key and URL
const API_KEY = "d623d2eeb389456bb0223aeedb962097";
const url = "https://newsapi.org/v2/everything?q=";

const nav = document.querySelector('nav');
let currentQuery = "India";
let currentPage = 1;
let pageSize = 20;

// Initial load
$(window).on("load", function () {
    fetchNewsAndBindData(currentQuery, 1);
});

// Function to fetch news
function fetchNews(query, page, callback) {
    var new_url = `${url}${query}&from=2023-10-28&sortBy=popularity&page=${page}&pageSize=${pageSize}&apiKey=${API_KEY}`
    $.ajax({
        url: new_url,
        method: "GET",
        success: (data) => {
            callback(data);
        },
        error: (error) => {
            console.log('Error: ', error);
        }
    });
}

// Update pagination when data is fetched
function fetchNewsAndBindData(query, page) {
    fetchNews(query, page, (data) => {
        bindData(data.articles);
        generatePagination(data.totalResults, page); // Pass the total number of results for pagination
    });
}

// Function to bind the fetched data to the UI
function bindData(articles) {
    const cardsContainer = document.getElementById("cards-container");
    const newsCard = document.getElementById("news-card");

    // Clear the cardsContainer
    cardsContainer.innerHTML = "";

    articles.forEach((article) => {
        if (!article.urlToImage || article.url.includes('smallbiztrends.com'))
            return;

        // Clone the newsCard and make it visible
        const cardClone = newsCard.cloneNode(true);
        cardClone.style.display = "block";

        fillDataInCard(cardClone, article);
        cardsContainer.appendChild(cardClone);

        // Add an event listener to open the article details card
        cardClone.addEventListener("click", () => {
            openArticleDetails(article, "_blank");
        });
    });
}

// Function to populate data into the news card
function fillDataInCard(cardClone, article) {
    const newsImg = cardClone.querySelector("#news-img");
    const newsTitle = cardClone.querySelector("#news-title");
    // const newsSource = cardClone.querySelector("#news-source");
    // const newsDesc = cardClone.querySelector("#news-desc");

    newsImg.src = article.urlToImage;
    newsTitle.innerHTML = article.title;
    // newsDesc.innerHTML = article.description;

    const date = new Date(article.publishedAt).toLocaleString("en-US", {
        timeZone: "Asia/Kolkata",
    });

    // newsSource.innerHTML = `${article.source.name} · ${date}`;
}

function openArticleDetails(article) {
    // Construct the URL of the article details page with the article URL as a query parameter
    const articleURL = article.url;
    const articleDetailsURL = `article.html?url=${encodeURIComponent(articleURL)}`;

    // Open the article details page in a new tab
    const articleDetailsWindow = window.open(articleDetailsURL, "_blank");

    // Populate the card structure in the new page with the article data
    articleDetailsWindow.addEventListener("DOMContentLoaded", () => {
        const articleDetailsDocument = articleDetailsWindow.document;

        const articleImg = articleDetailsDocument.getElementById("article-img");
        const articleTitle = articleDetailsDocument.getElementById("article-title");
        const articleSource = articleDetailsDocument.getElementById("article-source");
        const articleDesc = articleDetailsDocument.getElementById("article-desc");

        articleImg.src = article.urlToImage;
        articleTitle.innerHTML = article.title;
        const date = new Date(article.publishedAt).toLocaleString("en-US", {
            timeZone: "Asia/Kolkata",
        });
        articleSource.innerHTML = `${article.source.name} · ${date}`;
        articleDesc.innerHTML = article.description;

    });
}


// Initialize the selected navigation item
let curSelectedNav = null;

// Add an event listener to the 'nav' element to handle navigation item clicks
nav.addEventListener('click', (event) => {
    if (event.target.classList.contains('nav-item')) {
        const id = event.target.id;
        onNavItemClick(id);
    }
});

// Function to fetch news when a navigation item is clicked
function onNavItemClick(id) {
    currentQuery = id; // Update the current query
    currentPage = 1; // Always start from the first page when a navigation item is clicked
    fetchNewsAndBindData(currentQuery, currentPage);
    const navItem = document.getElementById(id);

    curSelectedNav.classList.remove("active");
    curSelectedNav = navItem;
    curSelectedNav.classList.add("active");
}

// Event listener for the search button
const searchButton = document.getElementById("search-button");
const searchText = document.getElementById("search-text");

searchButton.addEventListener("click", () => {
    const newQuery = searchText.value;
    if (!newQuery) return;
    currentQuery = newQuery; // Update the current query
    currentPage = 1; // Always start from the first page when a new query is entered
    fetchNewsAndBindData(currentQuery, currentPage);

    curSelectedNav.classList.remove("active");
    curSelectedNav = null;
});

// Change the background color to grey on hover
$(".news-card").hover(function () {
    $(this).css("background-color", "grey");
}, function () {
    $(this).css("background-color", "initial"); // Reset the background color
});

$(document).ready(function () {
    $('.card').hover(
        function () {
            // Mouse enter (hover in) event
            $(this).css({
                "box-shadow": "1px 1px 8px #f00",
                "background-color": "#f9fdff",
                "transform": "translateY(-2px)"
            }).addClass("rotate"); // Add a class to trigger rotation animation
            // $(this).fadeOut(100)
        },
        function () {
            // Mouse leave (hover out) event
            $(this).css({
                "box-shadow": "0 0 4px #d4ecff",
                "background-color": "#fff",
                "transform": "translateY(0)"
            }).removeClass("rotate"); // Remove the class to stop the rotation animation
            // $(this).fadeIn(500)
        }
    );
});
