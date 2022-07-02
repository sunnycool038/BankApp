using System.Text.RegularExpressions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectTemplate.Models;
using ProjectTemplate.Services.Interfaces;

namespace ProjectTemplate.Controllers
{
    [ApiController]
    [Route("api/v3/[controller]")]
    public class TransactionController:ControllerBase
    {
        private ITransactionService _transactionService;
        IMapper _mapper;
        public TransactionController(ITransactionService transactionService, IMapper mapper)
        {
            _transactionService = transactionService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("create_new_transaction")]
        public IActionResult CreateNewTransaction([FromBody]RequestTransactionDTO transactionRequest)
        {
            if(!ModelState.IsValid) return BadRequest(transactionRequest);
            var trannsaction = _mapper.Map<Transaction>(transactionRequest);
            return Ok(_transactionService.CreateNewTransaction(trannsaction));
        }

        [HttpPost]
        [Route("make_deposit")]
        public IActionResult MakeDeposit(string AccountNumber, decimal Amount, string TransactionPin)
        {
            if (!Regex.IsMatch(AccountNumber, @"^[0][1-9]\d{9}$|^[1-9]\d{9}$")) return BadRequest("Account number hust be 10 digit");
            return Ok(_transactionService.MakeDeposit(AccountNumber,Amount,TransactionPin));
        }

        [HttpPost]
        [Route("make_withdrawal")]
        public IActionResult MakeWithdrawal(string AccountNumber, decimal Amount, string TransactionPin)
        {
            if (!Regex.IsMatch(AccountNumber, @"^[0][1-9]\d{9}$|^[1-9]\d{9}$")) return BadRequest("Account number hust be 10 digit");
            return Ok(_transactionService.MakeWithdrawal(AccountNumber,Amount,TransactionPin));

        }

        [HttpPost]
        [Route("make_fund_transfer")]
        public IActionResult MakeFundsTransfer(string FromAccount, string ToAccount, decimal Amount, string TransactionPin)
        {
            if ((!Regex.IsMatch(FromAccount, @"^[0][1-9]\d{9}$|^[1-9]\d{9}$")) || (!Regex.IsMatch(ToAccount, @"^[0][1-9]\d{9}$|^[1-9]\d{9}$"))) return BadRequest("Account number hust be 10 digit");
            return Ok(_transactionService.MakeFundsTransfer(FromAccount,ToAccount,Amount,TransactionPin));
        }
    }
}