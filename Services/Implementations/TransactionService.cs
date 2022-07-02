using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using ProjectTemplate.Models;
using ProjectTemplate.DAL;
using System.Text;
using ProjectTemplate.Services.Interfaces;
using projectTemplate.utils;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace ProjectTemplate.Services.Implementations
{
    public class TransactionService : ITransactionService
    {
        private YouBankingDbContext _dbContext;
        ILogger<TransactionService> _logger;
        private AppSetting _setting;
        private string _OurBankSettlementAccount;
        private readonly IAccountService _accountService;
        public TransactionService(YouBankingDbContext dbContext, ILogger<TransactionService> logger, IOptions<AppSetting> setting, IAccountService accountService)
        {
            _dbContext = dbContext;
            _logger = logger;
            _setting = setting.Value;
            _accountService = accountService;
            _OurBankSettlementAccount = _setting.OurBankSettlementAccount;
        }
        public Response CreateNewTransaction(Transaction Transaction)
        {
            Response response = new Response();
            _dbContext.Transaction.Add(Transaction);
            _dbContext.SaveChanges();
            response.ResponseCode = "00";
            response.ResponseMessage = "trannsaction created successfully";
            response.Data = null!;
            return response;
        }

        public Response FindTransactionByDate(DateTime Date)
        {
            Response response = new Response();
            var trannsaction = _dbContext.Transaction.Where(x => x.TransactionDate == Date).ToList();
            response.ResponseCode = "00";
            response.ResponseMessage = "trannsaction created successfully";
            response.Data = trannsaction;
            return response;
        }

        public Response MakeDeposit(string AccountNumber, decimal Amount, string TransactionPin)
        {
            Response response = new Response();
            Account sourceAccount;
            Account destinationAccount;
            Transaction transaction = new Transaction();
            var authUser = _accountService.Authenticate(AccountNumber, TransactionPin);
            if (authUser == null) throw new ApplicationException("invalid credentials");
            try
            {
                sourceAccount = _accountService.GetByAccountNumber(_OurBankSettlementAccount);
                destinationAccount = _accountService.GetByAccountNumber(AccountNumber);
                sourceAccount.CurrentAccountBalance -= Amount;
                destinationAccount.CurrentAccountBalance += Amount;
                if ((_dbContext.Entry(sourceAccount).State == Microsoft.EntityFrameworkCore.EntityState.Modified) &&
                (_dbContext.Entry(destinationAccount).State == Microsoft.EntityFrameworkCore.EntityState.Modified))
                {
                    transaction.TransactionStatus = Transtatus.Success;
                    response.ResponseCode = "00";
                    response.ResponseMessage = "Transaction successful";
                    response.Data = null!;
                }
                else
                {
                    transaction.TransactionStatus = Transtatus.Failed;
                    response.ResponseCode = "02";
                    response.ResponseMessage = "Transaction Failed";
                    response.Data = null!;
                }
            }
            catch (System.Exception ex)
            {

                _logger.LogError($"AN ERROR OCCURED... =>{ex.Message}");
            }
            transaction.TransactionType = TranType.Deposit;
            transaction.TransactionSourceAccount = _OurBankSettlementAccount;
            transaction.TransactionDestinationAccount = AccountNumber;
            transaction.TransactionAmount = Amount;
            transaction.TransactionDate = DateTime.Now;
            transaction.TransactionParticulars = $"NEW TRANSACTION FROM SOURCE{JsonConvert.SerializeObject(transaction.TransactionSourceAccount)} TO DESTINATION ACCOUNT =>{JsonConvert.SerializeObject(transaction.TransactionDestinationAccount)} ON DATE =>{transaction.TransactionDate} FOR AMOUNT =>{JsonConvert.SerializeObject(transaction.TransactionAmount)} TRANSACTION TYPE =>{transaction.TransactionType}TRANSACTION STATUS =>{transaction.TransactionStatus}";
            _dbContext.Transaction.Add(transaction);
            _dbContext.SaveChanges();
            return response;
        }

        public Response MakeWithdrawal(string AccountNumber, decimal Amount, string TransactionPin)
        {
            Response response = new Response();
            Account sourceAccount;
            Account destinationAccount;
            Transaction transaction = new Transaction();
            var authUser = _accountService.Authenticate(AccountNumber, TransactionPin);
            if (authUser == null) throw new ApplicationException("invalid credentials");
            try
            {
                sourceAccount = _accountService.GetByAccountNumber(AccountNumber);
                destinationAccount = _accountService.GetByAccountNumber(_OurBankSettlementAccount);
                sourceAccount.CurrentAccountBalance -= Amount;
                destinationAccount.CurrentAccountBalance += Amount;
                if ((_dbContext.Entry(sourceAccount).State == Microsoft.EntityFrameworkCore.EntityState.Modified) &&
                (_dbContext.Entry(destinationAccount).State == Microsoft.EntityFrameworkCore.EntityState.Modified))
                {
                    transaction.TransactionStatus = Transtatus.Success;
                    response.ResponseCode = "00";
                    response.ResponseMessage = "Transaction successful";
                    response.Data = null!;
                }
                else
                {
                    transaction.TransactionStatus = Transtatus.Failed;
                    response.ResponseCode = "02";
                    response.ResponseMessage = "Transaction Failed";
                    response.Data = null!;
                }
            }
            catch (System.Exception ex)
            {

                _logger.LogError($"AN ERROR OCCURED... =>{ex.Message}");
            }
            transaction.TransactionType = TranType.Withdrawal;
            transaction.TransactionSourceAccount = _OurBankSettlementAccount;
            transaction.TransactionDestinationAccount = AccountNumber;
            transaction.TransactionAmount = Amount;
            transaction.TransactionDate = DateTime.Now;
            transaction.TransactionParticulars = $"NEW TRANSACTION FROM SOURCE{JsonConvert.SerializeObject(transaction.TransactionSourceAccount)} TO DESTINATION ACCOUNT =>{JsonConvert.SerializeObject(transaction.TransactionDestinationAccount)} ON DATE =>{transaction.TransactionDate} FOR AMOUNT =>{JsonConvert.SerializeObject(transaction.TransactionAmount)} TRANSACTION TYPE =>{transaction.TransactionType}TRANSACTION STATUS =>{transaction.TransactionStatus}";
            _dbContext.Transaction.Add(transaction);
            _dbContext.SaveChanges();
            return response;
        }

        public Response MakeFundsTransfer(string FromAccount, string ToAccount, decimal Amount, string TransactionPin)
        {
            Response response = new Response();
            Account sourceAccount;
            Account destinationAccount;
            Transaction transaction = new Transaction();
            var authUser = _accountService.Authenticate(FromAccount, TransactionPin);
            if (authUser == null) throw new ApplicationException("invalid credentials");
            try
            {
                sourceAccount = _accountService.GetByAccountNumber(FromAccount);
                destinationAccount = _accountService.GetByAccountNumber(ToAccount);
                sourceAccount.CurrentAccountBalance -= Amount;
                destinationAccount.CurrentAccountBalance += Amount;
                if ((_dbContext.Entry(sourceAccount).State == Microsoft.EntityFrameworkCore.EntityState.Modified) &&
                (_dbContext.Entry(destinationAccount).State == Microsoft.EntityFrameworkCore.EntityState.Modified))
                {
                    transaction.TransactionStatus = Transtatus.Success;
                    response.ResponseCode = "00";
                    response.ResponseMessage = "Transaction successful";
                    response.Data = null!;
                }
                else
                {
                    transaction.TransactionStatus = Transtatus.Failed;
                    response.ResponseCode = "02";
                    response.ResponseMessage = "Transaction Failed";
                    response.Data = null!;
                }
            }
            catch (System.Exception ex)
            {

                _logger.LogError($"AN ERROR OCCURED... =>{ex.Message}");
            }
            transaction.TransactionType = TranType.Transfer;
            transaction.TransactionSourceAccount = FromAccount;
            transaction.TransactionDestinationAccount = ToAccount;
            transaction.TransactionAmount = Amount;
            transaction.TransactionDate = DateTime.Now;
            transaction.TransactionParticulars = $"NEW TRANSACTION FROM SOURCE{JsonConvert.SerializeObject(transaction.TransactionSourceAccount)} TO DESTINATION ACCOUNT =>{JsonConvert.SerializeObject(transaction.TransactionDestinationAccount)} ON DATE =>{transaction.TransactionDate} FOR AMOUNT =>{JsonConvert.SerializeObject(transaction.TransactionAmount)} TRANSACTION TYPE =>{JsonConvert.SerializeObject(transaction.TransactionType)}TRANSACTION STATUS =>{transaction.TransactionStatus}";
            _dbContext.Transaction.Add(transaction);
            _dbContext.SaveChanges();
            return response;
        }
    }
}