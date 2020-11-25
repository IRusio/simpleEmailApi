using System.ComponentModel.DataAnnotations;

namespace EmailApi.Models
{
    public class RawTemplate
    {
        public string TemplateName { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Body { get; set; }
    }
}