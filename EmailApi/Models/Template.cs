using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmailApi.Models
{
    public class Template
    {
        public int Id { get; set; }
        public string TemplateName { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Body { get; set; }
        //TODO: Possible extension to return data whos are required to fill expectedValue 
        //[NotMapped]
        //public Dictionary<string, string> FieldsRequiredInTemplate { get; set; } = new Dictionary<string, string>();

        public Template()
        {
        }

        public Template(string templateName, string subject, string body)
        {
            TemplateName = templateName;
            Subject = subject;
            Body = body;
        }
    }
}