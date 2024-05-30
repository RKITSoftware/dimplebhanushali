using Microsoft.AspNetCore.Mvc;

namespace Developer_Exception_Page.Controllers
{
    /// <summary>
    /// Controller for handling various error scenarios.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class HandleErrorController : ControllerBase
    {
        /// <summary>
        /// Endpoint to simulate division by zero error.
        /// </summary>
        /// <param name="num1">The numerator.</param>
        /// <param name="num2">The denominator.</param>
        /// <returns>Result of the division.</returns>
        [HttpGet("DivideByZero")]
        public IActionResult DivideByZero(int num1, int num2)
        {
            // Division by zero exception
            return Ok($"Result : {num1 / num2}");
        }

        /// <summary>
        /// Endpoint to simulate null reference error.
        /// </summary>
        /// <returns>Length of a null string.</returns>
        [HttpGet("NullReference")]
        public IActionResult NullReference()
        {
            // Null reference exception
            string str = null;
            return Ok($"Length of string: {str.Length}");
        }

        /// <summary>
        /// Endpoint to simulate index out of bounds error.
        /// </summary>
        /// <returns>Fourth element of an array with 3 elements.</returns>
        [HttpGet("IndexOutOfBound")]
        public IActionResult IndextOutOfBound()
        {
            // Out of range exception
            int[] numbers = { 1, 2, 3 };
            return Ok($"Fourth element: {numbers[3]}");
        }

        /// <summary>
        /// Endpoint to simulate format error.
        /// </summary>
        /// <returns>Success message.</returns>
        [HttpGet("IncoorectFormat")]
        public IActionResult Format()
        {
            // Format exception
            int.Parse("abc");
            return Ok("Parsed successfully");
        }
    }
}
