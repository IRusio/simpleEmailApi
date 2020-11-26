using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;

namespace EmailApi.Application
{
    public class MailKitRepository : IEmailRepository
    {
        private readonly IConfiguration _configuration;
        public MailKitRepository(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public async Task<bool> SendEmailAsync(MimeMessage message)
        {
            try
            {
                var login = _configuration["Email:email"];
                var password = _configuration["Email:password"];
                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(_configuration["Email:server"], int.Parse(_configuration["Email:port"]), SecureSocketOptions.StartTlsWhenAvailable);
                await smtp.AuthenticateAsync(login, password);
                await smtp.SendAsync(message);
                await smtp.DisconnectAsync(true);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public async Task<MimeMessage> PrepareMessage(string fromEmail, string toEmail, string subject, string body, Dictionary<string, string> contextToFill = null)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(fromEmail));
            email.To.Add(MailboxAddress.Parse(toEmail));
            email.Subject = ReplaceTemplateValueContent(subject, contextToFill);
            email.Body = new TextPart(TextFormat.Plain) { Text = ReplaceTemplateValueContent(body, contextToFill)};
            return email;
        }

        /// <summary>
        /// This function replace {{value}} text from template values from contentToFill parameter
        /// </summary>
        /// <param name="text"></param>
        /// <param name="contentToFill"></param>
        /// <returns></returns>
        public static string ReplaceTemplateValueContent(string text, Dictionary<string, string> contentToFill)
        {
            var findValueInText = new Regex(@"{{([a-zA-Z]*)}}");
            var foundValuesToReplace = findValueInText.Matches(text);
            foreach (Match match in foundValuesToReplace)
            {
                var rawText = match.Value.Remove(match.Value.Length - 2, 2).Remove(0, 2);
                text = text.Replace(
                    match.Value,
                    contentToFill.ContainsKey(rawText)? contentToFill[rawText]: null
                    );
            }
            return text;
        }
    }
}