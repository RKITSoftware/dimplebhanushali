// API key and URL
const API_KEY = "d623d2eeb389456bb0223aeedb962097";
const url = "https://newsapi.org/v2/everything?q=";


// Initial load
$(window).on("load", function () {
    fetchNews("India", function (data) {
        bindData(data.articles);
    });
});

// Function to fetch news and provide data through a callback
function fetchNews(query, callback) {
    $.ajax({
        url: `${url}${query}&from=2023-10-28&sortBy=popularity&apiKey=${API_KEY}`,
        method: "GET",
        success: (data) => {
            // Call the callback function with the fetched data
            callback(data);
        },
        error: (error) => {
            console.log('Error: ', error);
        }
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
    });
}
// Function to populate data into the news card
function fillDataInCard(cardClone, article) {
    const newsImg = cardClone.querySelector("#news-img");
    const newsTitle = cardClone.querySelector("#news-title");
    const newsSource = cardClone.querySelector("#news-source");
    const newsDesc = cardClone.querySelector("#news-desc");

    newsImg.src = article.urlToImage;
    newsTitle.innerHTML = article.title;
    newsDesc.innerHTML = article.description;

    const date = new Date(article.publishedAt).toLocaleString("en-US", {
        timeZone: "Asia/Kolkata",
    });

    newsSource.innerHTML = `${article.source.name} · ${date}`;

    cardClone.addEventListener("click", () => {
        window.open(article.url, "_blank");
    });
}

// Initialize the selected navigation item
let curSelectedNav = null;

// Function to fetch news when a navigation item is clicked
function onNavItemClick(id) {
    fetchNews(id, (data) => {
        bindData(data.articles);
    });

    const navItem = document.getElementById(id);
    curSelectedNav?.classList.remove("active");
    curSelectedNav = navItem;
    curSelectedNav.classList.add("active");
}

// Event listener for the search button
const searchButton = document.getElementById("search-button");
const searchText = document.getElementById("search-text");

searchButton.addEventListener("click", () => {
    const query = searchText.value;
    if (!query) return;
    fetchNews(query, (data) => {
        bindData(data.articles);
    });

    curSelectedNav?.classList.remove("active");
    curSelectedNav = null;
});