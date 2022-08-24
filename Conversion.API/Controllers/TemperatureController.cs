using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Conversion.Core;
using Conversion.Core.Contracts;

namespace Conversion.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TemperatureController : ControllerBase
    {
        private readonly ILogger<TemperatureController> _logger;
        private readonly ITemperatureConverter _temperatureConverter;

        public TemperatureController(ILogger<TemperatureController> logger, ITemperatureConverter temperatureConverter)
        {
            _logger = logger;
            _temperatureConverter = temperatureConverter;
        }

        [HttpGet]
        [Route("celsius/{value}")]
        public async Task<IActionResult> ConvertToCelsius(decimal value)
        {
            return Ok(await _temperatureConverter.ConvertToCelsius(value));
        }

        [HttpGet]
        [Route("fahrenheit/{value}")]
        public async Task<IActionResult> ConvertToFahrenheit(decimal value)
        {
            return Ok(await _temperatureConverter.ConvertToFahrenheit(value));
        }
    }
}
