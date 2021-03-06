using System.Collections.Generic;
using System.Threading.Tasks;
using EmailApi.Models;

namespace EmailApi.Application
{
    public interface IEmailHistoryRepository
    {
        public Task AddMessage(string email, int templateId, string subject, string body);
        public Task<IEnumerable<EmailHistory>> GetEmailHistory();
        public Task<IEnumerable<EmailHistory>> GetEmailHistoryByEmailReceiver(string userEmail);
    }
}