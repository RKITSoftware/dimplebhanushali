namespace Logging.MAL
{
    /// <summary>
    /// Data transfer object for representing error responses.
    /// </summary>
    public class ErrorResponseDto
    {
        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the detailed error information.
        /// </summary>
        public string Details { get; set; }
    }
}
