using System.Collections.Generic;
using System.Threading.Tasks;
using EmailApi.Db;
using EmailApi.Models;
using EmailApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EmailApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class EmailTemplatesController : ControllerBase
    {
        private readonly ILogger<EmailController> _logger;
        private readonly EmailTemplatesService _emailTemplatesService;

        public EmailTemplatesController(ILogger<EmailController> logger, EmailContext context)
        {
            _logger = logger;
            _emailTemplatesService = new EmailTemplatesService(context);
        }

        [HttpPost]
        public async Task AddNewTemplate()
        {
            
        }

        [HttpGet]
        public async Task<IEnumerable<Template>> GetTemplates()
        {
            return _emailTemplatesService.GetTemplates();
        }
    }
}