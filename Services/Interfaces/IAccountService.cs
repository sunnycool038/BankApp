using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using ProjectTemplate.Models;

namespace ProjectTemplate.Services.Interfaces
{
    
    public interface IAccountService
    {
        Account Authenticate(string AccountNumber, string Pin);
        IEnumerable<Account> GetAllAcounts();
        Account Create(Account Account,string Pin, String ComfirmPin);
        void Update(Account Account,string Pin );//pin=null
        void Delete(int Id);
        Account GetById(int Id );
        Account GetByAccountNumber(string AccountNumber );
    }   
}