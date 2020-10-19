using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using GetLinksAnywhere.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GetLinksAnywhere.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessingController : ControllerBase
    {
        private readonly IFinderService _finderService;
        private readonly ILogger<ProcessingController> _logger;

        public ProcessingController(IFinderService finderService, 
            ILogger<ProcessingController> logger)
        {
            _finderService = finderService;
            _logger = logger;
        }

        [HttpPost]
        [Consumes("text/plain")]
        public async Task<IActionResult> FindLinksInText([FromBody] string data,
            CancellationToken cancellationToken)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            if (string.IsNullOrWhiteSpace(data))
                return NoContent();

            var result = await _finderService.FindAllLinks(data, 
                cancellationToken: cancellationToken);

            stopWatch.Stop();
            _logger.LogInformation($"Request has been proceeded in {stopWatch.Elapsed.Seconds}");

            return Ok(result);
        }
    }
}
