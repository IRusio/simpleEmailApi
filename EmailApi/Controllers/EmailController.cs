using System.Collections.Generic;
using System.Threading.Tasks;
using EmailApi.Db;
using EmailApi.Models;
using EmailApi.Services;
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
        private readonly IEmailService _emailService;
        private readonly IEmailTemplatesService _templatesService;
        private readonly EmailHistoryService _emailHistoryService;

        public EmailController(ILogger<EmailController> logger, IConfiguration configuration, EmailContext context)
        {
            _logger = logger;
            _configuration = configuration;
            _templatesService = new EmailTemplatesService(context);
            _emailService = new MailKitService(configuration);
            _emailHistoryService = new EmailHistoryService(context);
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail([FromBody] EmailWithName email)
        {
            var template = await _templatesService.GetTemplateByIdAsync(email.TemplateId);
            if (template == null)
                return NoContent();
            var message = await _emailService.PrepareMessage(_configuration["Email:email"], email.EmailAddress, template.Subject,
                template.Body, new Dictionary<string, string>() {{"name", email.Name}});
            await _emailService.SendEmailAsync(message);
            await _emailHistoryService.AddSendMessageToHistory(email.EmailAddress, email.TemplateId, message.Subject, message.TextBody);
            return Ok();
        }
        
        // [HttpPost]
        // public async Task<IActionResult> SendEmail([FromBody]Email email,[FromBody] Dictionary<string, string> parametersToReplace)
        // {
        //     var template = await _templatesService.GetTemplateByIdAsync(email.TemplateId);
        //     if (template == null)
        //         return NoContent();
        //     
        //     //currently is support only name especially for this task. If u want to do sth more, its simple must prepare 
        //     var message = await _emailService.PrepareMessage(_configuration["Email:email"], email.EmailAddress, template.Subject,
        //         template.Body, parametersToReplace);
        //     await _emailService.SendEmailAsync(message);
        //     await _emailHistoryService.AddSendMessageToHistory(email.EmailAddress, email.TemplateId, message.Subject, message.Body.ToString());
        //     return Ok();
        // }
    }
}