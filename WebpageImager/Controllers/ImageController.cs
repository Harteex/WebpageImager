using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebpageImager.Services;

namespace WebpageImager.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IHostedService _hostedService;
        private readonly ILogger<ImageController> _logger;

        public ImageController(IHostedService hostedService, ILogger<ImageController> logger)
        {
            _hostedService = hostedService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var chrome = HttpContext.RequestServices.GetService<ChromeService>();
            var image = chrome?.GetScreenshot();

            if (image == null)
            {
                return StatusCode(500);
            }

            return File(image, "image/png");
        }
    }
}
