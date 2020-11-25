using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using EmailApi.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EmailApi.Services
{
    public interface IEmailTemplatesService
    {
        public Task<Template> AddTemplateAsync(RawTemplate template);

        public Task<Template> GetTemplateByIdAsync(int templateId);
        //Possible task. Pagination etc.
        public Task<List<Template>> GetTemplatesAsync();
        public Task<Template> EditTemplateAsync(Template template);
        public Task<bool> DeleteTemplate(int templateId);
    }
}