using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EmailApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class EmailTemplatesController : ControllerBase
    {
        private readonly ILogger<EmailController> _logger;

        public EmailTemplatesController(ILogger<EmailController> logger)
        {
            _logger = logger;
        }

        public async Task AddNewTemplate()
        {
            
        }
    }
}