using System;
using System.Threading.Tasks;
using System.Web.Http;
using WebScrappingLibrary;

namespace WebScrapping_using_Base_Library.Controllers
{
    /// <summary>
    /// Controller for web scraping operations.
    /// </summary>
    public class WebScrapingController : ApiController
    {
        /// <summary>
        /// Scrapes a web page for links, paragraphs, images, and forms.
        /// </summary>
        /// <param name="url">The URL of the web page to scrape.</param>
        /// <returns>An IHttpActionResult containing the scraped data.</returns>
        [HttpGet]
        [Route("api/WebScraping/ScrapeWebPage")]
        public async Task<IHttpActionResult> ScrapeWebPage(string url)
        {
            try
            {
                // Create an instance of WebScrappingUtility
                var webScrappingUtility = new WebScrapingUtility();

                // Call the ScrapeWebPage method from the utility
                var result = await webScrappingUtility.ScrapeWebPage(url);

                // Return the result as JSON
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Return an error message for unexpected exceptions
                return InternalServerError(ex);
            }
        }
    }
}
