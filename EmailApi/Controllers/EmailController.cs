using System.Collections.Generic;
using System.Threading.Tasks;
using EmailApi.Application;
using EmailApi.Db;
using EmailApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;

namespace EmailApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly ILogger<EmailController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IEmailRepository _emailRepository;
        private readonly IEmailTemplatesRepository _templatesRepository;
        private readonly EmailHistoryRepository _emailHistoryRepository;

        public EmailController(ILogger<EmailController> logger, IConfiguration configuration, EmailContext context)
        {
            _logger = logger;
            _configuration = configuration;
            _templatesRepository = new EmailTemplatesRepository(context);
            _emailRepository = new MailKitRepository(configuration);
            _emailHistoryRepository = new EmailHistoryRepository(context);
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail([FromBody] EmailWithName email)
        {
            var template = await _templatesRepository.GetTemplateByIdAsync(email.TemplateId);
            if (template == null)
                return NoContent();
            var message = await _emailRepository.PrepareMessage(_configuration["Email:email"], email.EmailAddress, template.Subject,
                template.Body, new Dictionary<string, string>() {{"name", email.Name}});
            var result = await _emailRepository.SendEmailAsync(message);
            if (result == false)
                return BadRequest("Possible errors with login etc. Possible not correct credentials.");
            await _emailHistoryRepository.AddMessage(email.EmailAddress, email.TemplateId, message.Subject, message.TextBody);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetEmailHistory()
        {
            var result = await _emailHistoryRepository.GetEmailHistory();
            return Ok(result);
        }
        
        [HttpGet("{userEmail}")]
        public async Task<IActionResult> GetEmailHistory(string userEmail)
        {
            var result = await _emailHistoryRepository.GetEmailHistoryByEmailReceiver(userEmail);
            return Ok(result);
        }
    }
}