using System.Text.RegularExpressions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectTemplate.Models;
using ProjectTemplate.Services.Interfaces;

namespace ProjectTemplate.Controllers
{
    [ApiController]
    [Route("api/v3/[controller]")]
    public class AccountsControllers : ControllerBase
    {
        private readonly IAccountService _accountService;
        IMapper _mapper;
        public AccountsControllers(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("register_new_account")]
        public IActionResult RegisterNewAccount([FromBody] RegisterNewAccountModel newAccount)
        {
            if (!ModelState.IsValid) return BadRequest(newAccount);
            var account = _mapper.Map<Account>(newAccount);
            return Ok(_accountService.Create(account, newAccount.Pin, newAccount.ConfirmPin));
        }
        [HttpGet]
        [Route("get_all_accounts")]
        public IActionResult GetAllAccounts()
        {
            var account = _accountService.GetAllAcounts();
            var cleanedAccount = _mapper.Map<IList<GetAccountModel>>(account);
            return Ok(cleanedAccount);
        }

        [HttpPost]
        [Route("authenticate")]
        public IActionResult Authenticate([FromBody]AuthenticateModel model)
        {
            if(!ModelState.IsValid) return BadRequest(model);
            return Ok(_accountService.Authenticate(model.AccountNumber, model.Pin));
        }

        [HttpGet]
        [Route("get_by_account_number")]
        public IActionResult GetByAccountNumber(string AccountNumber)
        {
            if (!Regex.IsMatch(AccountNumber, @"^[0][1-9]\d{9}$|^[1-9]\d{9}$")) return BadRequest("Account number hust be 10 digit");
            var account = _accountService.GetByAccountNumber(AccountNumber);
            var cleanedAccount = _mapper.Map<GetAccountModel>(account);
            return Ok(cleanedAccount);
        }

        [HttpGet]
        [Route("get_account_by_id")]
        public IActionResult GetById(int Id)
        {
            var account = _accountService.GetById(Id);
            var cleanedAccount = _mapper.Map<GetAccountModel>(account);
            return Ok(cleanedAccount);
        }

        [HttpPut]
        [Route("update_account")]
        public IActionResult UpdateAccount([FromBody] UpdateAccountModel model)
        {
            if (!ModelState.IsValid) return BadRequest(model);
            var account = _mapper.Map<Account>(model);
            _accountService.Update(account, model.Pin);
            return Ok();

        }
    }
}