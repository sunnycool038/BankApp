using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTemplate.Models
{
    public class RequestTransactionDTO
    {
        public decimal TransactionAmount { get; set; }
        public string TransactionSourceAccount { get; set; } = null!;
        public string TransactionDestinationAccount { get; set; } = null!;
        public TranType TransactionType { get; set; } 
        public DateTime TransactionDate { get; set; }
    }
}