using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Search_API.BL
{
    /// <summary>
    /// Service class for interacting with the SerpApi to perform Google Finance searches.
    /// </summary>
    public class GoogleFinanceSearchService
    {
        private const string ApiKey = "328a6a85df319a6707d2b0ebdd30f0e050b0ea48858f8fb69becf27d422a6bb0";
        private const string Engine = "google_finance";
        private const string SerpApiBaseUrl = "https://serpapi.com/search";

        /// <summary>
        /// Performs a Google Finance search using SerpApi and returns the search results.
        /// </summary>
        /// <param name="query">The search query.</param>
        /// <returns>An asynchronous task returning the dynamic search results.</returns>
        public async Task<dynamic> SearchGoogleFinance(string query)
        {
            // Validate the query parameter
            if (string.IsNullOrWhiteSpace(query))
            {
                throw new ArgumentException("Query parameter is required", nameof(query));
            }

            // Construct the SerpApi URL
            string apiUrl = $"{SerpApiBaseUrl}?engine={Engine}&q={query}&api_key={ApiKey}";

            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    // Make an asynchronous GET request to the SerpApi endpoint
                    HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        // Read and deserialize the response data
                        byte[] data = await response.Content.ReadAsByteArrayAsync();
                        string responseData = Encoding.UTF8.GetString(data);
                        dynamic finalData = JsonConvert.DeserializeObject<dynamic>(responseData);
                        return finalData;
                    }
                    else
                    {
                        // Handle non-successful response
                        throw new HttpRequestException($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception accordingly
                throw;
            }
        }
    }
}
