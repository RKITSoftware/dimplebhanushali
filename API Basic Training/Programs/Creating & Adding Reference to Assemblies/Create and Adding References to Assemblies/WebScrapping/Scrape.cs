using HtmlAgilityPack;
using System;
using System.Net.Http;

namespace WebScrapping
{
    public class Scrape
    {
        public void ScrapWebPage(string url)
        {
            WebScrape(url);
        }

        private void WebScrape(string uri)
        {
            string url = uri;

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {
                    string htmlContent = response.Content.ReadAsStringAsync().Result;

                    HtmlDocument htmlDocument = new HtmlDocument();
                    htmlDocument.LoadHtml(htmlContent);

                    var links = htmlDocument.DocumentNode.SelectNodes("//a[@href]");
                    var paragraphs = htmlDocument.DocumentNode.SelectNodes("//p");
                    var images = htmlDocument.DocumentNode.SelectNodes("//img");
                    var forms = htmlDocument.DocumentNode.SelectNodes("//form");

                    if (links != null)
                    {
                        Console.WriteLine("Links => ");
                        foreach (var link in links)
                        {
                            Console.WriteLine(link.GetAttributeValue("href", ""));
                        }
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine();
                    }

                    // Extract all text content within the <p> tags
                    if (paragraphs != null)
                    {
                        Console.WriteLine("Paragraphs => ");
                        foreach (var paragraph in paragraphs)
                        {
                            Console.WriteLine(paragraph.InnerText);
                        }
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine();
                    }

                    // Extract attributes of specific elements
                    if (images != null)
                    {
                        Console.WriteLine("Images => ");
                        foreach (var image in images)
                        {
                            Console.WriteLine("Image Source: " + image.GetAttributeValue("src", ""));
                            Console.WriteLine("Alt Text: " + image.GetAttributeValue("alt", ""));
                        }
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine();
                    }

                    // Extract form elements and their attributes
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
                        Console.WriteLine();
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("Failed to retrieve the page. Status code: " + response.StatusCode);
                }
                Console.WriteLine("All Done");
            }
        }

    }
}
