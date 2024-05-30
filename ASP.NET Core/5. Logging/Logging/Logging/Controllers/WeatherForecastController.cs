using Microsoft.AspNetCore.Mvc;

namespace Logging.Controllers
{
    /// <summary>
    /// API Controller for handling weather forecast requests.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
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
        /// Handles GET requests for weather forecast.
        /// </summary>
        /// <returns>Returns null after logging a critical error message.</returns>
        [HttpGet(Name = "GetWeatherForecast")]
        public dynamic Get()
        {
            try
            {
                throw new Exception("Custom Exception");
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "An exception occurred while getting the weather forecast.");
                return null;
            }
        }
    }
}
