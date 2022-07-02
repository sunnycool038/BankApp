using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using ProjectTemplate.Models;

namespace ProjectTemplate.Services.Interfaces
{
    
    public interface ITransactionService
    {
        Response CreateNewTransaction(Transaction Transaction);
        Response FindTransactionByDate(DateTime Date);
        Response MakeDeposit(string AccountNumber, decimal Amount, string TransactionPin);
        Response MakeWithdrawal(string AccountNumber, decimal Amount, string TransactionPin);
        Response MakeFundsTransfer(string FromAccount,string ToAccount, decimal Amount, string TransactionPin);
    }   
}