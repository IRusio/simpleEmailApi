using System.Collections.Generic;
using System.Linq;
using EmailApi.Db;
using EmailApi.Models;

namespace EmailApi.Services
{
    public class EmailTemplatesService : IEmailTemplatesService
    {
        private readonly EmailContext _context;

        public EmailTemplatesService(EmailContext context)
        {
            _context = context;
        }

        public Template AddTemplate(RawTemplate template)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Template> GetTemplates()
        {
            return _context.Templates.ToList();
        }

        public Template EditTemplate(Template template)
        {
            throw new System.NotImplementedException();
        }

        public bool DeleteTemplate(int templateId)
        {
            throw new System.NotImplementedException();
        }
    }
}