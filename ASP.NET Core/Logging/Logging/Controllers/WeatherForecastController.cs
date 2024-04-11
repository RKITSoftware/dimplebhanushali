using Microsoft.AspNetCore.Mvc;

namespace Logging.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public dynamic Get()
        {
            try
            {
                throw new Exception("Custom Exception");
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                return null;
            }
        }

    }
}