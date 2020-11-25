using System.Collections.Generic;
using System.Threading.Tasks;
using EmailApi.Models;

namespace EmailApi.Services
{
    public interface IEmailHistoryService
    {
        public Task AddSendMessageToHistory(string email, int templateId, string subject, string body);
        public Task<IEnumerable<EmailHistory>> GetEmailHistory();
    }
}