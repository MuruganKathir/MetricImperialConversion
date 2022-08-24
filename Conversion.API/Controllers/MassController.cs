using System.Threading.Tasks;
using Conversion.Core;
using Conversion.Core.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Conversion.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MassController : ControllerBase
    {
        private readonly ILogger<MassController> _logger;
        private readonly IMassConverter _massConverter;

        public MassController(ILogger<MassController> logger, IMassConverter massConverter)
        {
            _logger = logger;
            _massConverter = massConverter;
        }

        [HttpGet]
        [Route("kilograms/{value}")]
        public async Task<IActionResult> ConvertToKilograms(decimal value)
        {
            return Ok(await _massConverter.ConvertToKilograms(value));
        }

        [HttpGet]
        [Route("pounds/{value}")]
        public async Task<IActionResult> ConvertToPounds(decimal value)
        {
            return Ok(await _massConverter.ConvertToPounds(value));
        }
    }
}