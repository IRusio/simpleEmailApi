﻿using System.ComponentModel.DataAnnotations;

namespace EmailApi.Models
{
    public class Email
    {
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        public int TemplateId { get; set; }
        

        public Email()
        {
        }
    }
}