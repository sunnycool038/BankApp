using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProjectTemplate.Models
{
    [Table("Transactions")]
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        public string TransactionUniqueReference { get; set; } = null!;
        public decimal TransactionAmount { get; set; }
        public Transtatus TransactionStatus { get; set; }
        public bool isSuccessful => TransactionStatus.Equals(Transtatus.Success);
        public string TransactionSourceAccount { get; set; } = null!;
        public string TransactionDestinationAccount { get; set; } = null!;
        public string TransactionParticulars { get; set; } = null!;
        public TranType TransactionType { get; set; } 

        public DateTime TransactionDate { get; set; }

        public Transaction(){
            TransactionUniqueReference = $"{Guid.NewGuid().ToString().Replace("-","").Substring(1,27)}";
        }
    }

    public enum Transtatus
    {
        Failed,
        Success,
        Error
    }

    public enum TranType
    {
        Deposit,
        Withdrawal,
        Transfer
    }

}
