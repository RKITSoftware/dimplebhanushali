using HtmlAgilityPack;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebScrappingLibrary
{
    /// <summary>
    /// Utility class for web scraping operations.
    /// </summary>
    public class WebScrapingUtility
    {
        /// <summary>
        /// Scrapes a web page for links, paragraphs, images, and forms.
        /// </summary>
        /// <param name="url">The URL of the web page to scrape.</param>
        /// <returns>An object containing the scraped data.</returns>
        public async Task<object> ScrapeWebPage(string url)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        // Read HTML content from the response
                        string htmlContent = await response.Content.ReadAsStringAsync();
                        HtmlDocument htmlDocument = new HtmlDocument();
                        htmlDocument.LoadHtml(htmlContent);

                        // Extract links using XPath
                        var links = htmlDocument.DocumentNode.SelectNodes("//a[@href]");

                        // Extract paragraphs using XPath
                        var paragraphs = htmlDocument.DocumentNode.SelectNodes("//p");

                        // Extract images using XPath
                        var images = htmlDocument.DocumentNode.SelectNodes("//img");

                        // Extract forms using XPath
                        var forms = htmlDocument.DocumentNode.SelectNodes("//form");

                        // Create an object to store the results
                        var result = new
                        {
                            Links = links?.Select(link => link.GetAttributeValue("href", "")),
                            Paragraphs = paragraphs?.Select(paragraph => paragraph.InnerText),
                            Images = images?.Select(image => new
                            {
                                Source = image.GetAttributeValue("src", ""),
                                AltText = image.GetAttributeValue("alt", "")
                            }),
                            Forms = forms?.Select(form => new
                            {
                                Action = form.GetAttributeValue("action", ""),
                                Method = form.GetAttributeValue("method", "")
                            })
                        };

                        return result;
                    }
                    else
                    {
                        throw new Exception($"Failed to retrieve the page. Status code: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred during web scraping.", ex);
            }
        }
    }
}
