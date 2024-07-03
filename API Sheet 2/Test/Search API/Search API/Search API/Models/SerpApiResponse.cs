using Newtonsoft.Json;
using System.Collections.Generic;

namespace Search_API.Models
{
    /// <summary>
    /// Represents the response structure from SerpApi.
    /// </summary>
    public class SerpApiResponse
    {
        /// <summary>
        /// Gets or sets the list of organic search results.
        /// </summary>
        [JsonProperty("organic_results")]
        public List<SerpApiResult> OrganicResults { get; set; }
    }
}
