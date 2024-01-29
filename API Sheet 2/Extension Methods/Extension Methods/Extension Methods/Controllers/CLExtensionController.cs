using ExtensionMethod;
using System.Web.Http;

namespace Extension_Methods.Controllers
{
    /// <summary>
    /// Controller for handling extension methods.
    /// </summary>
    [RoutePrefix("api")]
    public class CLExtensionController : ApiController
    {
        /// <summary>
        /// Inverts the case of the first letter in a string.
        /// </summary>
        /// <param name="value">The input string.</param>
        /// <returns>A string with the case of the first letter inverted.</returns>
        [HttpPost]
        [Route("ChangeFirstLetter/{value}")]
        public IHttpActionResult ChangeFirstLetterCase(string value)
        {
            try
            {
                // Call the InvertFirstLetterCase extension method
                string result = value.InvertFirstLetterCase();

                // Return the result as JSON
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                // Return an error message for unexpected exceptions
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Checks if an integer value is greater than or equal to 1000.
        /// </summary>
        /// <param name="value">The input integer value.</param>
        /// <returns>True if the value is greater than or equal to 1000; otherwise, false.</returns>
        [HttpPost]
        [Route("CheckGreater/{value}")]
        public IHttpActionResult CheckGreater(int value)
        {
            try
            {
                // Call the IsGreaterThanOrEqualTo extension method
                bool result = value.IsGreaterThanOrEqualTo();

                // Return the result as JSON
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                // Return an error message for unexpected exceptions
                return InternalServerError(ex);
            }
        }
    }
}
