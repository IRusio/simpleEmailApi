﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        public Dictionary<string, string> FieldsRequiredInTemplate { get; set; }
    }
}