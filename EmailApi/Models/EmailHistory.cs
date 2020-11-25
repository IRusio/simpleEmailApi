using System;
using System.ComponentModel.DataAnnotations;

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
        public string TemplateId { get; set; }
        public int SendStatus { get; set; }
    }
}