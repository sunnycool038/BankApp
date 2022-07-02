using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace ProjectTemplate.Models
{
    public class RegisterNewAccountModel
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        //public string AccountName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        //public decimal CurrentAccountBalance { get; set; }
        public AccountType AccountType { get; set; }
        //public string AccountNumberGenerated { get; set; } = null!;
        //public byte[] PinHash { get; set; } = null!;
        //public byte[] PinSalt { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public DateTime DateLastUpdated { get; set; }
        [Required]
        [RegularExpression(@"^[0-9]{4}$", ErrorMessage ="Pin must not be more than 4 digits")]
        public string Pin { get; set; }=null!;
        [Required]
        [Compare("Pin")]
        public string ConfirmPin { get; set; }=null!;
    }
}