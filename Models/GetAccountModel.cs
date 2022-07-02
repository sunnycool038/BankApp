using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace ProjectTemplate.Models
{
    public class GetAccountModel
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string AccountName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public decimal CurrentAccountBalance { get; set; }
        public AccountType AccountType { get; set; }
        public string AccountNumberGenerated { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public DateTime DateLastUpdated { get; set; }
    }   
}