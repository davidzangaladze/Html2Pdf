using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace Html2Pdf.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PrintController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        public PrintController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("pdf")]
        public IActionResult Index([FromBody] string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return BadRequest("content is empty");
            }

            try
            {
                var decodedContent = HttpUtility.UrlDecode(content);
                var enginePath = _configuration["BrowserPath"];
                var commandArgument = _configuration["CommandArgument"];

                var result = Converter.GeneratePdfBytes(enginePath, commandArgument, decodedContent);

                return File(result, "application/pdf");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
    }
}
