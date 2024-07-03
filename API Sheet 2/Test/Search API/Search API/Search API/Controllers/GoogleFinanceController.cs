using Search_API.BL;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Search_API.Controllers
{
    /// <summary>
    /// Controller for handling Google Finance searches using the SerpApi.
    /// </summary>
    public class GoogleFinanceController : ApiController
    {
        private readonly GoogleFinanceSearchService searchService;

        /// <summary>
        /// Constructor to initialize the controller with the GoogleFinanceSearchService.
        /// </summary>
        public GoogleFinanceController()
        {
            // You can use Dependency Injection to inject the service if needed
            this.searchService = new GoogleFinanceSearchService();
        }

        /// <summary>
        /// Performs a Google Finance search using the SerpApi and returns the search results.
        /// </summary>
        /// <param name="query">The search query.</param>
        /// <returns>IHttpActionResult containing the search results.</returns>
        [HttpGet]
        [Route("api/serpapi/search")]
        public async Task<IHttpActionResult> Search([FromUri] string query)
        {
            try
            {
                // Call the GoogleFinanceSearchService to perform the search
                var searchResults = await searchService.SearchGoogleFinance(query);
                return Ok(searchResults);
            }
            catch (ArgumentException ex)
            {
                // Handle invalid arguments
                return BadRequest(ex.Message);
            }
            catch (HttpRequestException ex)
            {
                // Handle HTTP request errors
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                return InternalServerError(ex);
            }
        }
    }
}
