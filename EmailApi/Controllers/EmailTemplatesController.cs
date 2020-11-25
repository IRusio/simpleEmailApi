using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EmailApi.Db;
using EmailApi.Models;
using EmailApi.Services;
using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> AddNewTemplate(RawTemplate template)
        {
            return Ok(await _emailTemplatesService.AddTemplateAsync(template));
        }

        [HttpGet]
        public async Task<IActionResult> GetTemplates()
        {
            return Ok(await _emailTemplatesService.GetTemplatesAsync());
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTemplate(Template template)
        {
            try
            {
                var result = await _emailTemplatesService.EditTemplateAsync(template);
                return Ok(result);
            }
            catch (BadHttpRequestException e)
            {
                return NoContent();
            }
        }

        [HttpDelete("{templateId}")]
        public async Task<IActionResult> DeleteTemplate(int templateId)
        {
            var result = await _emailTemplatesService.DeleteTemplate(templateId);
            if (!result)
            {
                return NoContent();
            }

            return Ok();
        }
    }
}