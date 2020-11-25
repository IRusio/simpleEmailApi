using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EmailApi.Services;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using Xunit;

namespace EmailApiUnitTests
{
    public class EmailMessageBuilder
    {
        [Theory]
        [InlineData("test@test.test", "test", "{{test}}", "test")]
        [InlineData("test@test.test", "test", "{{text}}", "")]
        public async Task EmailContentBuilder(string email, string subject, string body, string expectedBody)
        {
            //Arrange
            var emailService = new MailKitService(new ConfigurationBuilder().Build());
            var messageToCompare = new MimeMessage();
            messageToCompare.From.Add(MailboxAddress.Parse(email));
            messageToCompare.To.Add(MailboxAddress.Parse(email));
            messageToCompare.Subject = subject;
            messageToCompare.Body = new TextPart(TextFormat.Plain) {Text = expectedBody};
            //Act
            var data = await emailService.PrepareMessage(email, email, subject, body,
                new Dictionary<string, string>() {{"test", "test"}});
            //Assert
            Assert.Equal(messageToCompare.TextBody, data.TextBody);
            Assert.Equal(messageToCompare.Subject, data.Subject);
            Assert.Equal(messageToCompare.From, data.From);
            Assert.Equal(messageToCompare.To, data.To);
        }
    }
}