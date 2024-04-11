using Microsoft.AspNetCore.Mvc;
using NLog;

namespace NlogDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

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
