using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Developer_Exception_Page.Error
{
    /// <summary>
    /// Model class representing an error response.
    /// </summary>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)] // For Caching Response
    [IgnoreAntiforgeryToken] // Prevents CSRF, No need to validate token for showing error page.
    public class ErrorModel : PageModel
    {
        /// <summary>
        /// Gets or sets the request identifier associated with the error.
        /// </summary>
        public string? RequestId { get; set; }

        /// <summary>
        /// Gets a value indicating whether to show the request identifier.
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        /// <summary>
        /// Gets or sets the exception message describing the error.
        /// </summary>
        public string ExceptionMsg { get; set; }
    }
}
