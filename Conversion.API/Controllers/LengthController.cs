using System.Threading.Tasks;
using Conversion.Core;
using Conversion.Core.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Conversion.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LengthController : ControllerBase
    {
        private readonly ILogger<LengthController> _logger;
        private readonly ILengthConverter _lengthConverter;

        public LengthController(ILogger<LengthController> logger, ILengthConverter lengthConverter)
        {
            _logger = logger;
            _lengthConverter = lengthConverter;
        }

        [HttpGet]
        [Route("kilometers/{value}")]
        public async Task<IActionResult> ConvertToKilometers(decimal value)
        {
            return Ok(await _lengthConverter.ConvertToKilometers(value));
        }

        [HttpGet]
        [Route("miles/{value}")]
        public async Task<IActionResult> ConvertToMiles(decimal value)
        {
            return Ok(await _lengthConverter.ConvertToMiles(value));
        }
    }
}