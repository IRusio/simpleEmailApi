using System.Collections.Generic;
using System.Threading.Tasks;
using MimeKit;

namespace EmailApi.Application
{
    public interface IEmailRepository
    {
        public Task<bool> SendEmailAsync(MimeMessage message);

        public Task<MimeMessage> PrepareMessage(string fromEmail, string toEmail, string subject, string body,
            Dictionary<string, string> contextToFill = null);
    }
}