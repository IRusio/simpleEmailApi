using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EmailApi.Application;
using EmailApi.Db;
using EmailApi.Models;
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
        private readonly EmailTemplatesRepository _emailTemplatesRepository;

        public EmailTemplatesController(ILogger<EmailController> logger, EmailContext context)
        {
            _logger = logger;
            _emailTemplatesRepository = new EmailTemplatesRepository(context);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewTemplate(RawTemplate template)
        {
            return Ok(await _emailTemplatesRepository.AddTemplateAsync(template));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTemplateById(int id)
        {
            return Ok(await _emailTemplatesRepository.GetTemplateByIdAsync(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetTemplates()
        {
            return Ok(await _emailTemplatesRepository.GetTemplatesAsync());
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTemplate(Template template)
        {
            try
            {
                var result = await _emailTemplatesRepository.EditTemplateAsync(template);
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
            var result = await _emailTemplatesRepository.DeleteTemplate(templateId);
            return !result? (IActionResult) NoContent(): Ok();
        }
    }
}