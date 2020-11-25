using System.Collections;
using System.Collections.Generic;
using EmailApi.Models;

namespace EmailApi.Services
{
    public interface IEmailTemplatesService
    {
        public Template AddTemplate(RawTemplate template);
        //Possible task. Pagination etc.
        public IEnumerable<Template> GetTemplates();
        public Template EditTemplate(Template template);
        public bool DeleteTemplate(int templateId);
    }
}