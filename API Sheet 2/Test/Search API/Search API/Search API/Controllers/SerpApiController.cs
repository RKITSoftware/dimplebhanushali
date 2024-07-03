using Search_API.BL;
using Search_API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace Search_API.Controllers
{
    /// <summary>
    /// Controller for interacting with the SerpApi to perform search and retrieve organic search results.
    /// </summary>
    [RoutePrefix("api/SERP")]
    public class SerpApiController : ApiController
    {
        private readonly SerpApiSearchService searchService;

        /// <summary>
        /// Constructor to initialize the controller with the SerpApiSearchService.
        /// </summary>
        public SerpApiController()
        {
            // You can use Dependency Injection to inject the service if needed
            this.searchService = new SerpApiSearchService();
        }

        /// <summary>
        /// Performs a search using SerpApi and returns a list of organic search results.
        /// </summary>
        /// <param name="query">The search query.</param>
        /// <returns>List of organic search results.</returns>
        [HttpGet, Route("Search")]
        public async Task<List<SerpApiResult>> Search(string query)
        {
            try
            {
                // Call the SerpApiSearchService to perform the search
                var searchResults = await searchService.SearchAsync(query);
                return searchResults;
            }
            catch (Exception ex)
            {
                // Handle exceptions
                // Log or return an appropriate response
                return null;
            }
        }
    }

    
}
