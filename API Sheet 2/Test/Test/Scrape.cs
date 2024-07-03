using System;
using System.Net.Http;
using HtmlAgilityPack;

namespace WebScrapping
{
    /// <summary>
    /// Class for web scraping functionality.
    /// </summary>
    public class Scrape
    {
        /// <summary>
        /// Performs web scraping logic for the specified URL.
        /// </summary>
        /// <param name="uri">The URL of the web page to scrape.</param>
        public void ScrapWebPage(string url)
        {
            WebScrape(url);
        }

        /// <summary>
        /// Performs web scraping logic for the specified URL.
        /// </summary>
        /// <param name="uri">The URL of the web page to scrape.</param>
        private void WebScrape(string uri)
        {
            string url = uri;

            // Using HttpClient to make HTTP requests
            using (HttpClient client = new HttpClient())
            {
                // Send GET request to the specified URL
                HttpResponseMessage response = client.GetAsync(url).Result;

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Read HTML content from the response
                    string htmlContent = response.Content.ReadAsStringAsync().Result;

                    // Use HtmlAgilityPack to parse HTML content
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

                    // Display extracted links
                    if (links != null)
                    {
                        Console.WriteLine("Links => ");
                        foreach (var link in links)
                        {
                            Console.WriteLine(link.GetAttributeValue("href", ""));
                        }
                        Console.WriteLine();
                    }

                    // Display extracted paragraphs
                    if (paragraphs != null)
                    {
                        Console.WriteLine("Paragraphs => ");
                        foreach (var paragraph in paragraphs)
                        {
                            Console.WriteLine(paragraph.InnerText);
                        }
                        Console.WriteLine();
                    }

                    // Display extracted images and their attributes
                    if (images != null)
                    {
                        Console.WriteLine("Images => ");
                        foreach (var image in images)
                        {
                            Console.WriteLine("Image Source: " + image.GetAttributeValue("src", ""));
                            Console.WriteLine("Alt Text: " + image.GetAttributeValue("alt", ""));
                        }
                        Console.WriteLine();
                    }

                    // Display extracted form elements and their attributes
                    if (forms != null)
                    {
                        Console.WriteLine("Form => ");
                        foreach (var form in forms)
                        {
                            Console.WriteLine("Form Action: " + form.GetAttributeValue("action", ""));
                            Console.WriteLine("Form Method: " + form.GetAttributeValue("method", ""));
                            // You can extract input fields, buttons, etc., within the form
                        }
                        Console.WriteLine();
                    }
                }
                else
                {
                    // Display an error message if the request fails
                    Console.WriteLine("Failed to retrieve the page. Status code: " + response.StatusCode);
                }

                // Display a message indicating the end of the process
                Console.WriteLine("All Done");
            }
        }
    }
}
