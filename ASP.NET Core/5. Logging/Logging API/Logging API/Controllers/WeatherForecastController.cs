using Microsoft.AspNetCore.Mvc;

namespace Logging_API.Controllers
{
    /// <summary>
    /// API Controller for handling weather forecast requests.
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
        /// <returns>An enumeration of <see cref="WeatherForecast"/> objects.</returns>
        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            // Log messages at different levels
            _logger.LogTrace("Trace Line");
            _logger.LogDebug("Debug Line");
            _logger.LogInformation("Information Line");
            _logger.LogWarning("Warning Line");
            _logger.LogError("Error Line");
            _logger.LogCritical("Event Log Critical Line");

            // Generate and return a list of weather forecasts
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
