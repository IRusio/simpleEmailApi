using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EmailApi.Db;
using EmailApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace EmailApi.Services
{
    public class EmailTemplatesService : IEmailTemplatesService
    {
        private readonly EmailContext _context;

        public EmailTemplatesService(EmailContext context)
        {
            _context = context;
        }

        public async Task<Template> AddTemplateAsync(RawTemplate rawTemplate)
        {
            var newTemplate = new Template(rawTemplate.TemplateName, rawTemplate.Subject, rawTemplate.Body);
            var result = await _context.Templates.AddAsync(newTemplate);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Template> GetTemplateByIdAsync(int templateId)
        {
            return await _context.Templates.SingleOrDefaultAsync(x => x.Id == templateId);
        }

        public Task<List<Template>> GetTemplatesAsync()
        {
            return _context.Templates.ToListAsync();
        }

        public async Task<Template> EditTemplateAsync(Template template)
        {
            var oldTemplate = await _context.Templates.SingleOrDefaultAsync(x => x.Id == template.Id);
            if (oldTemplate != null)
            {
                oldTemplate.Body = template.Body;
                oldTemplate.Subject = template.Subject;
                oldTemplate.TemplateName = template.TemplateName;
                await _context.SaveChangesAsync();
                return oldTemplate;
            }
            else
            {
                throw new BadHttpRequestException("Old template not found");
            }
        }

        public async Task<bool> DeleteTemplate(int templateId)
        {
            var template = await _context.Templates.SingleOrDefaultAsync(x => x.Id == templateId);
            if (template == null)
                return false;
            else
            {
                _context.Templates.Remove(template);
                await _context.SaveChangesAsync();
                return true;
            }
        }
    }
}