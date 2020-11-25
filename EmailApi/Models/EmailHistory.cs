using System;
using System.ComponentModel.DataAnnotations;

namespace EmailApi.Models
{
    public class EmailHistory
    {
        public int Id { get; set; }
        public string Email { get; set; }
        [Required]
        public string Receiver { get; set; }
        [Required]
        public string Body { get; set; }
        [Required]
        public DateTime SendDate { get; set; }
        [Required]
        public string TemplateId { get; set; }
        public int SendStatus { get; set; }
    }
}