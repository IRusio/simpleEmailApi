using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailApi.Db;
using EmailApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EmailApi.Application
{
    public class EmailHistoryRepository : IEmailHistoryRepository
    {
        private readonly EmailContext _context;

        public EmailHistoryRepository(EmailContext context)
        {
            _context = context;
        }
        
        public async Task AddMessage(string email, int templateId, string subject, string body)
        {
            var template = await _context.Templates.SingleOrDefaultAsync(x => x.Id == templateId);
            var historyEntity = new EmailHistory(email, subject, body, DateTime.Now, template);
            await _context.AddAsync(historyEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<EmailHistory>> GetEmailHistory()
        {
            return await _context.EmailHistory.ToListAsync();
        }

        public async Task<IEnumerable<EmailHistory>> GetEmailHistoryByEmailReceiver(string userEmail)
        {
            return await _context.EmailHistory.Where(x => x.ReceiverEmailAddress == userEmail).ToListAsync();
        }
    }
}