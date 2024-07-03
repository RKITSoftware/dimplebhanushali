using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Search_API.Models;
using Search_API.Controllers;

namespace Search_API.BL
{
    /// <summary>
    /// Business logic class for interacting with the SerpApi to perform search and retrieve organic search results.
    /// </summary>
    public class SerpApiSearchService
    {
        private const string ApiKey = "328a6a85df319a6707d2b0ebdd30f0e050b0ea48858f8fb69becf27d422a6bb0";
        private const string SerpApiUrl = "https://serpapi.com/search";

        /// <summary>
        /// Performs a search using SerpApi and returns a list of organic search results.
        /// </summary>
        /// <param name="query">The search query.</param>
        /// <returns>List of organic search results.</returns>
        public async Task<List<SerpApiResult>> SearchAsync(string query)
        {
            using (var httpClient = new HttpClient())
            {
                var apiUrl = $"{SerpApiUrl}?api_key={ApiKey}&q={query}";

                HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    var searchResults = JsonConvert.DeserializeObject<SerpApiResponse>(responseData);
                    return searchResults.OrganicResults;
                }
                else
                {
                    // Handle non-successful response
                    return null;
                }
            }
        }
    }
}
