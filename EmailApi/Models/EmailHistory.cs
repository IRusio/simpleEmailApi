using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace EmailApi.Models
{
    public class EmailHistory
    {
        public int Id { get; set; }
        [Required]
        [EmailAddress]
        public string ReceiverEmailAddress { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Body { get; set; }
        [Required]
        public DateTime SendDate { get; set; }
        [Required]
        public Template Template { get; set; }
        public int SendStatus { get; set; }

            public EmailHistory(string receiverEmailAddress, string subject, string body, DateTime sendDate, Template template, int sendStatus = 0)
        {
            ReceiverEmailAddress = receiverEmailAddress;
            Subject = subject;
            Body = body;
            Template = template;
            SendDate = sendDate;
            SendStatus = sendStatus;
        }

        public EmailHistory()
        {
        }
    }
}