using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProjectTemplate.Models
{
    [Table("Account")]
    public class Account
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
        [JsonIgnore]
        public byte[] PinHash { get; set; } = null!;
        [JsonIgnore]
        public byte[] PinSalt { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public DateTime DateLastUpdated { get; set; }

        Random Rand = new Random();
        public Account()
        {
            AccountNumberGenerated = Convert.ToString((long)Math.Floor(Rand.NextDouble()*9_000_000_000L +1_000_000_000L));
            AccountName = $"{FirstName} {LastName}";
        }
    }

    public enum AccountType
    {
        Savings,
        Current,
        Corporate,
        Government
    }
}