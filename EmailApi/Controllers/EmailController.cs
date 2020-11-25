using System.Threading.Tasks;
using EmailApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace EmailApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly ILogger<EmailController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        public EmailController(ILogger<EmailController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _emailService = new MailKitService(configuration);
        }

        // [HttpPost]
        // public Task<IActionResult> SendEmail(string email, string name, int templateId)
        // {
        //     
        // }
    }
}