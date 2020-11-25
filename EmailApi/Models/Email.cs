using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmailApi.Models
{
    public class Email
    {
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        public int TemplateId { get; set; }
        [Required] 
        public Dictionary<string, string> ParametersToReplace { get; set; }
        

        public Email()
        {
        }
    }
}