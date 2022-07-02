using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace ProjectTemplate.Models
{
    public class UpdateAccountModel
    {
        [Key]
        public int Id { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime DateLastUpdated { get; set; }
        [Required]
        [RegularExpression(@"^[0-9]{4}$", ErrorMessage ="Pin must not be more than 4 digits")]
        public string Pin { get; set; }=null!;
        [Required]
        [Compare("Pin")]
        public string ConfirmPin { get; set; }=null!;
    }   
}