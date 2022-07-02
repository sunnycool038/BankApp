using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace ProjectTemplate.Models
{
    public class AuthenticateModel{
        [Required]
        [RegularExpression(@"^[0][1-9]\d{9}$|^[1-9]\d{9}$")]
        public string AccountNumber { get; set; } =null!;
        public string Pin { get; set; } =null!;
    }
}