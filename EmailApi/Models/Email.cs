using System.ComponentModel.DataAnnotations;

namespace EmailApi.Models
{
    public class Email
    {
        [EmailAddress]
        public string EmailAddress { get; set; }

        public Email(string emailAddress)
        {
            EmailAddress = emailAddress;
        }
    }
}