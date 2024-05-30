using Logging_API;
using Microsoft.AspNetCore.Mvc;

namespace NlogDemo.Controllers
{
    /// <summary>
    /// API Controller for handling weather forecast requests and generating various types of errors for logging.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        /// <summary>
        /// Predefined summaries of weather conditions.
        /// </summary>
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        /// <summary>
        /// The logger instance for logging messages.
        /// </summary>
        private readonly ILogger<WeatherForecastController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeatherForecastController"/> class.
        /// </summary>
        /// <param name="logger">The logger instance for logging messages.</param>
        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Handles GET requests for weather forecasts.
        /// </summary>
        /// <returns>An array of weather forecasts.</returns>
        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        /// <summary>
        /// Simulates a division operation and logs the result.
        /// </summary>
        /// <param name="a">The dividend.</param>
        /// <param name="b">The divisor.</param>
        /// <returns>An HTTP response with the division result.</returns>
        [HttpGet("Division")]
        public IActionResult Division(decimal a, decimal b)
        {
            try
            {
                decimal ans = a / b;
                _logger.LogError($"Answer = {ans}");
                return Ok(ans);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Simulates a null reference exception and logs it.
        /// </summary>
        /// <returns>An HTTP response with the error message.</returns>
        [HttpGet("NullReference")]
        public IActionResult NullReference()
        {
            try
            {
                // Null reference exception
                string str = null;
                return Ok($"Length of string: {str.Length}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Simulates an index out of bound exception and logs it.
        /// </summary>
        /// <returns>An HTTP response with the error message.</returns>
        [HttpGet("IndexOutOfBound")]
        public IActionResult IndextOutOfBound()
        {
            try
            {
                // Out of range exception
                int[] numbers = { 1, 2, 3 };
                return Ok($"Fourth element: {numbers[3]}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Simulates a format exception and logs it.
        /// </summary>
        /// <returns>An HTTP response with the error message.</returns>
        [HttpGet("IncoorectFormat")]
        public IActionResult Format()
        {
            try
            {
                // Format exception
                int.Parse("abc");
                return Ok("Parsed successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Simulates a critical error and logs it.
        /// </summary>
        /// <returns>An HTTP response with the error message.</returns>
        [HttpGet("CriticalError")]
        public IActionResult CriticalError()
        {
            try
            {
                // Simulate critical error
                throw new InvalidOperationException("Critical error occurred!");
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Simulates an unknown error and logs it.
        /// </summary>
        /// <returns>An HTTP response with the error message.</returns>
        [HttpGet("UnknownError")]
        public IActionResult UnknownError()
        {
            try
            {
                // Simulate unknown error
                throw new Exception("Unknown error occurred!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

    }
}
