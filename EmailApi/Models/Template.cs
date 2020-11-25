using System.ComponentModel.DataAnnotations;

namespace EmailApi.Models
{
    public class Template
    {
        public int Id { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Body { get; set; }
    }
}