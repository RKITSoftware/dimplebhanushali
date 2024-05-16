namespace Historical_Events.Models
{
    /// <summary>
    /// Represents a generic response structure for API responses.
    /// </summary>
    /// <typeparam name="T">Type of the data to be included in the response.</typeparam>
    public class Response
    {
        /// <summary>
        /// Indicates whether the operation was successful or not.
        /// </summary>
        public bool isError { get; set; } = false;

        /// <summary>
        /// Contains the data returned by the API operation.
        /// </summary>
        public dynamic Data { get; set; }

        /// <summary>
        /// Provides additional information or error messages related to the API operation.
        /// </summary>
        public string Message { get; set; }
    }
}