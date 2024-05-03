using System.Data;

namespace Certificate_Generator.Models
{
    /// <summary>
    /// Represents a generic response structure for API responses.
    /// </summary>
    public class Response
    {
        /// <summary>
        /// Indicates whether the operation was successful or not.
        /// </summary>
        public bool HasError { get; set; } = false;

        /// <summary>
        /// Provides additional information or error messages related to the API operation.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Contains the data returned by the API operation.
        /// </summary>
        public dynamic Data { get; set; }
    }
}
