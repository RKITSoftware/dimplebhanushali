namespace MiddlewareWithFilters.Models
{
    /// <summary>
    /// Response Model
    /// </summary>
    public class Response
    {
        /// <summary>
        /// Has Error
        /// </summary>
        public bool HasError { get; set; } = false;

        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Data
        /// </summary>
        public dynamic Data { get; set; }
    }
}
