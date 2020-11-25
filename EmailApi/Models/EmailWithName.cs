using System.ComponentModel.DataAnnotations;

namespace EmailApi.Models
{
    public class EmailWithName
    {
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        public int TemplateId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}