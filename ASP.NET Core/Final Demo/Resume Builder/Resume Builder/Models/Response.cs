namespace Resume_Builder.Models
{
    /// <summary>
    /// Represents a generic response model.
    /// </summary>
    public class Response
    {
        /// <summary>
        /// Gets or sets a value indicating whether the response has error.
        /// </summary>
        public bool HasError { get; set; } = false;

        /// <summary>
        /// Gets or sets the message associated with the response.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the data associated with the response.
        /// </summary>
        public dynamic Data { get; set; }
    }
}
