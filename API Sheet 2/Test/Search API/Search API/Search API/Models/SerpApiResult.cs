namespace Search_API.Models
{
    /// <summary>
    /// Represents an individual search result from SerpApi.
    /// </summary>
    public class SerpApiResult
    {
        /// <summary>
        /// Gets or sets the title of the search result.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the link associated with the search result.
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// Gets or sets the snippet or brief description of the search result.
        /// </summary>
        public string Snippet { get; set; }
    }
}
